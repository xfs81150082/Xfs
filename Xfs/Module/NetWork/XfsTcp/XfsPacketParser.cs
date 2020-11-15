using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Xfs
{
    public class XfsPacketParser : XfsComponent
    {
        #region Properties        
        public Socket Socket { get; private set; }                ///创建一个套接字，用于储藏代理服务端套接字，与客户端通信///客户端Socket 
        public bool IsRunning { get; set; }
        #endregion

        #region byte[] Bytes        
        private byte[] RecvBuffer { get; set; }                 ///接收缓冲区   
        private byte[] SendBuffer { get; set; }                 ///发送缓冲区   
        private int BufferSize { get; set; } = 1024;
        private int RecvLength { get; set; }
        private List<byte> RecvBuffList { get; set; } = new List<byte>();    ///接收字节列表  
        private List<byte> SendBuffList { get; set; } = new List<byte>();    ///发送字节列表  
        private int iBytesHead { get; set; } = 8;
        private int surHL { get; set; } 
        private int surBL { get; set; } = 0;
        private bool isHead { get; set; } = true;
        private bool isBody { get; set; } = false;
        #endregion
        #region ReceiveMsg
        public void BeginReceiveMessage(object obj)
        {          
            surHL = iBytesHead;
            Socket = obj as Socket;
            RecvBuffer = new byte[BufferSize];
            IsRunning = true;
            Socket.BeginReceive(RecvBuffer, 0, BufferSize, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), this);
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                RecvLength = Socket.EndReceive(ar);
                if (RecvLength == 0)
                {
                    ///发送端关闭
                    Console.WriteLine("{0} 发送端{1}连接关闭", XfsTimeHelper.CurrentTime(), Socket.RemoteEndPoint);
                    IsRunning = false;
                    //Dispose();
                    return;
                }
                else
                {
                    AddRange(RecvBuffList, RecvBuffer, RecvLength);
                }
                ///触发事件 解析缓存池RecvBuffList<byte> 读取数据字节
                ParsingBytes();
                ///继续接收来自来客户端的数据  
                Socket.BeginReceive(RecvBuffer, 0, BufferSize, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " " + ex.ToString());
                IsRunning = false;
                //Dispose();
            }
        }
        private void ParsingBytes()
        {
            ///将本次要接收的消息头字节数置0
            //int iBytesHead;
            ///接收消息体（消息体的长度存储在消息头的0至4索引位置的字节里）
            byte[] HeadBytes = null;
            ///将本次要剪切的字节数置0
            int iBytesBody = 0;

            try
            {
                if (isHead)
                {
                    ///如果当前需要接收的字节数小于缓存池RecvBuffList，进行下一步操作
                    if (surHL <= RecvBuffList.Count)
                    {
                        //iBytesHead = surHL;
                        surHL = 0;
                    }
                    if (surHL == 0)
                    {
                        isHead = false;
                        isBody = true;
                        /////接收消息体（消息体的长度存储在消息头的0至4索引位置的字节里）
                        //byte[] HeadBytes = new byte[iBytesHead];
                        HeadBytes = new byte[iBytesHead];
                        ///将接收到的字节数的消息头保存到HeadBytes，//减去已经接收到的字节数
                        CutTo(RecvBuffList, HeadBytes, 0, iBytesHead);

                        ///拿出包头中前四个字节，此字节是操代码
                        int opcode = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(HeadBytes, 0));
                    
                        ///拿出包头中后四个字节，此字节是包体长度
                        int msgLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(HeadBytes, 4));
                     
                        surBL = msgLength;

                        ///一个消息包包头HeadBytes消息包 接收完毕，下面解析消息包包身 
                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " Recv , Opcode : {0} . BodyBytes.Length:{1}", opcode, msgLength);
                    }
                }
                if (isBody)
                {
                    ///如果当前需要接收的字节数大于0，则循环接收
                    if (surBL <= RecvBuffList.Count)
                    {
                        iBytesBody = surBL;
                        surBL = 0;                    ///归零进入下一步操作
                    }
                    if (surBL == 0)
                    {
                        isBody = false;
                        isHead = true;
                        surHL = 4;
                        ///一个消息包接收完毕，解析消息包
                        byte[] BodyBytes = new byte[iBytesBody];
                        CutTo(RecvBuffList, BodyBytes, 0, iBytesBody);

                        ///接受处理完整的字节数据包，包括包头和包身
                        //this.RecvBufferBytes(this, HeadBytes, BodyBytes);
                        //this.SessionRecvBufferByte(this, HeadBytes, BodyBytes);

                        this.OnReadRecv(this, HeadBytes, BodyBytes);

                        HeadBytes = null;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + ex.ToString());
                //Dispose();
            }
        }

        private Action<object, byte[], byte[]> readCallback;

        public event Action<object, byte[] , byte[]> ReadCallback
        {
            add
            {
                this.readCallback += value;
            }
            remove
            {
                this.readCallback -= value;
            }
        }
        void OnReadRecv(object obj, byte[] HeadBytes, byte[] BodyBytes)
        {
            this.readCallback.Invoke(obj, HeadBytes, BodyBytes);
        }


        #endregion
        #region AddRange        
        void CutTo(List<byte> BuffList, byte[] bytes, int bytesoffset, int size)
        {
            BuffList.CopyTo(0, bytes, bytesoffset, size);
            BuffList.RemoveRange(0, size);
        }/// 提取数据        
        void AddRange(List<byte> BuffList, byte[] buffer, int length)
        {
            byte[] temByte = new byte[length];
            Array.Copy(buffer, 0, temByte, 0, length);
            BuffList.AddRange(temByte);
        }/// 队列数据
        #endregion
        #region SendString
        public void SendString(string mvcString)
        {
            if (null == Socket.Handle || !Socket.Connected)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " 连接已中断！！！");
                IsRunning = false;
                return;
            }
            ///将字符串(string)转换成字节(byte)
            byte[] jsonsByte = Encoding.UTF8.GetBytes(mvcString);
            ///消息包长度
            int sendLength = iBytesHead + jsonsByte.Length;
            ///定义数据包（消息头长度 + 消息体长度）
            byte[] MsgsByte = new byte[sendLength];

            ///包头先存入消息长度数值4个字节 操作代码
            ushort opcode = 101;
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(opcode)).CopyTo(MsgsByte, 0);
        
            ///包头再存入消息长度数值4个字节 包体长度
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(jsonsByte.Length)).CopyTo(MsgsByte, 4);            

            ///包身然后存入信息体字节
            jsonsByte.CopyTo(MsgsByte, 8);
            AddRange(SendBuffList, MsgsByte, MsgsByte.Length);
            while (sendLength > 0)
            {
                try
                {
                    if (sendLength <= BufferSize)
                    {
                        byte[] temBytes = new byte[sendLength];
                        CutTo(SendBuffList, temBytes, 0, sendLength);
                        Socket.BeginSend(temBytes, 0, temBytes.Length, 0, new AsyncCallback(this.SendCallback), Socket);
                        sendLength = 0;
                    }
                    else
                    {
                        byte[] temBytes = new byte[BufferSize];
                        CutTo(SendBuffList, temBytes, 0, BufferSize);
                        sendLength -= BufferSize;
                        Socket.BeginSend(temBytes, 0, temBytes.Length, 0, new AsyncCallback(this.SendCallback), Socket);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + ex.ToString());
                    //Dispose();
                }
            }
        } ///发送信息给客户端
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " Sent {0} Bytes. ThreadId:{1}", bytesSent, Thread.CurrentThread.ManagedThreadId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + ex.ToString());
            }
        }
        #endregion
        #region   dispose OnConnect
        //public override void Dispose()
        //{
        //    base.Dispose();
        //    try
        //    {
        //        Socket.Shutdown(SocketShutdown.Both);
        //        IsRunning = false;
        //        Socket.Close();
        //        Socket = null;
        //        Console.WriteLine(XfsTimeHelper.CurrentTime() + " EcsId:" + InstanceId + " TmTcpSession释放资源");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(XfsTimeHelper.CurrentTime() + " " + ex.Message);
        //    }
        //}
        #endregion
        
    }
}
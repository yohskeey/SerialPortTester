using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortConnectModule.Model
{
    public class SerialPortControl
    {
        public SerialPort SP { get; }

        public SerialPortControl()
        {
            this.SP = new SerialPort();
        }

        public bool IsOpen => this.SP.IsOpen;
        public bool CDHolding => this.SP.CDHolding;
        public bool DsrHolding => this.SP.DsrHolding;
        public bool CtsHolding => this.SP.CtsHolding;


        /// <summary>
        /// ポートの一覧
        /// </summary>
        /// <returns></returns>
        public static string[] GetPortNames() => SerialPort.GetPortNames();

        public bool Connect(string port, int baudrate, int dataBits, int stopBits, int parity, int handshake, bool dtr, bool rts)
        {
            this.SP.PortName = port;
            this.SP.BaudRate = baudrate;
            this.SP.ReadBufferSize = 256;    //受信バッファサイズ
            this.SP.DataBits = dataBits;
            this.SP.StopBits = (StopBits)stopBits;     //ストップビット
            this.SP.Parity = (Parity)parity;        //パリティ
            this.SP.Handshake = (Handshake)handshake;  //フロー制御
            this.SP.DtrEnable = dtr;
            this.SP.RtsEnable = rts;

            this.SP.Open();
            

            return this.SP.IsOpen;
        }


        public void Close()
        {
            if (this.SP.IsOpen)
            {
                this.SP.Close();
            }
        }
    }
}

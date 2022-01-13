using Prism.Commands;
using Prism.Mvvm;
using SerialPortConnectModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortConnectModule.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get => this._message;
            set => SetProperty(ref this._message, value);
        }

        private SerialPortControl serialPortControl;
        public SerialPortControl SerialPortCtrl
        {
            get => this.serialPortControl;
            set => SetProperty(ref this.serialPortControl, value);
        }

        private string[] comboBoxItem;
        public string[] ComboBoxItem
        {
            get => this.comboBoxItem;
            set => SetProperty(ref this.comboBoxItem, value);
        }

        private int comboBoxSelectIndex;
        public int ComboBoxSelectIndex
        {
            get => this.comboBoxSelectIndex;
            set => SetProperty(ref this.comboBoxSelectIndex, value);
        }

        private int baudrate;
        public int BaudRate
        {
            get => this.baudrate;
            set => SetProperty(ref this.baudrate, value);
        }

        private int dataBits;
        public int DataBits
        {
            get => this.dataBits;
            set => SetProperty(ref this.dataBits, value);
        }

        public static string[] ComboBoxStopbits => new string[] { "None", "One", "Two", "OnePointFive" };
        private int comboBoxStopbitsSelectIndex;
        public int ComboBoxStopbitsSelectIndex
        {
            get => this.comboBoxStopbitsSelectIndex;
            set => SetProperty(ref this.comboBoxStopbitsSelectIndex, value);
        }

        public static string[] ComboBoxParity => new string[] { "None", "Odd", "Even", "Mark", "Space" };
        private int comboBoxParitySelectIndex;
        public int ComboBoxParitySelectIndex
        {
            get => this.comboBoxParitySelectIndex;
            set => SetProperty(ref this.comboBoxParitySelectIndex, value);
        }

        public static string[] ComboBoxHandshake => new string[] { "None", "XOnXOff", "RequestToSend", "RequestToSendXOnXOff" };
        private int comboBoxHandshakeSelectIndex;
        public int ComboBoxHandshakeSelectIndex
        {
            get => this.comboBoxHandshakeSelectIndex;
            set => SetProperty(ref this.comboBoxHandshakeSelectIndex, value);
        }

        private bool dtrEnable;
        public bool DtrEnable
        {
            get => this.dtrEnable;
            set => SetProperty(ref this.dtrEnable, value);
        }
        private bool rtsEnable;
        public bool RtsEnable
        {
            get => this.rtsEnable;
            set => SetProperty(ref this.rtsEnable, value);
        }

        private string connectLabel;
        public string ConnectLabel
        {
            get => this.connectLabel;
            set => SetProperty(ref this.connectLabel, value);
        }

        private bool portIsOpen;
        public bool PortIsOpen
        {
            get => this.portIsOpen;
            set
            {
                SetProperty(ref this.portIsOpen, value);
                this.ConnectLabel = value ? "close" : "Connect";
            }
        }
        private bool _CDHolding;
        public bool CDHolding
        {
            get => this._CDHolding;
            set => SetProperty(ref this._CDHolding, value);
        }
        private bool _DsrHolding;
        public bool DsrHolding
        {
            get => this._DsrHolding;
            set => SetProperty(ref this._DsrHolding, value);
        }
        private bool _CtsHolding;
        public bool CtsHolding
        {
            get => this._CtsHolding;
            set => SetProperty(ref this._CtsHolding, value);
        }

        public DelegateCommand Clicked { get; private set; }
        public DelegateCommand CheckClicked { get; private set; }


        public ViewAViewModel()
        {
            //パラメータ初期化
            this.Message = "View A from your Prism Module";
            this.SerialPortCtrl = new SerialPortControl();
            this.ComboBoxItem = SerialPortControl.GetPortNames();
            this.ComboBoxSelectIndex = 0;
            this.BaudRate = 2400;
            this.DataBits = 7;
            this.ComboBoxStopbitsSelectIndex = (int)System.IO.Ports.StopBits.Two;     //ストップビット
            this.ComboBoxParitySelectIndex = (int)System.IO.Ports.Parity.Even;        //パリティ
            this.ComboBoxHandshakeSelectIndex = (int)System.IO.Ports.Handshake.None;  //フロー制御
            this.DtrEnable = true;
            this.RtsEnable = true;
            this.PortIsOpen = false;

            this.Clicked = new DelegateCommand(
              OnConnectClick, // 実行される内容
              () => true); // 実行できるか？

            this.CheckClicked = new DelegateCommand(ReloadSerialParameter, () => true);

            this.SerialPortCtrl.SP.PinChanged += SP_PinChanged;
        }

        private void SP_PinChanged(object sender, System.IO.Ports.SerialPinChangedEventArgs e)
        {
            ReloadSerialParameter();
        }

        private void ReloadSerialParameter()
        {
            this.PortIsOpen = this.SerialPortCtrl.SP.IsOpen;
            if (this.PortIsOpen)
            {
                this.CDHolding = this.SerialPortCtrl.SP.CDHolding;
                this.DsrHolding = this.SerialPortCtrl.SP.DsrHolding;
                this.CtsHolding = this.SerialPortCtrl.SP.CtsHolding;
            }
            else
            {
                this.CDHolding = false;
                this.DsrHolding = false;
                this.CtsHolding = false;
            }
        }

        private void OnConnectClick()
        {
            if (this.PortIsOpen)
            {
                this.SerialPortCtrl.Close();
                ReloadSerialParameter();
            }
            else
            {
                this.SerialPortCtrl.Connect(
                    this.ComboBoxItem[this.ComboBoxSelectIndex],
                    this.BaudRate,
                    this.DataBits,
                    this.ComboBoxStopbitsSelectIndex,
                    this.ComboBoxParitySelectIndex,
                    this.ComboBoxHandshakeSelectIndex,
                    this.DtrEnable, this.RtsEnable);
                ReloadSerialParameter();
            }
        }
    }
}

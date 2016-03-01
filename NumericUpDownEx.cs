using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library
{
    /// <summary>NumericUpDownの拡張クラス。
    /// </summary>
    public class NumericUpDownEx : NumericUpDown
    {
        // コンストラクタ
        /// <summary>コンストラクタ
        /// </summary>
        public NumericUpDownEx()
        {
            this.TextAlign = HorizontalAlignment.Right;
        }

        // 定数
        /// <summary>ウィンドウメッセージ - WM_MOUSEWHEEL
        /// </summary>
        private const int WM_MOUSEWHEEL = 0x20A;

        // フィールド
        // プロパティ
        /// <summary>Int32に変換した値を取得する。
        /// </summary>
        [Browsable(false)]
        public int Int32Value
        {
            get
            {
                return (int)this.Value;
            }
        }

        /// <summary>表示されている文字列
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        /// <summary>値を取得また設定する。Textプロパティが空の場合はnullを返す。nullを設定された場合は内部の値に0、Textプロパティに空を設定する。
        /// </summary>
        public new decimal? Value
        {
            get
            {
                decimal n;
                if (decimal.TryParse(this.Text, out n))
                {
                    return base.Value;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value.HasValue)
                {
                    base.Value = value.Value;
                }
                else
                {
                    base.Value = 0;
                    this.Text = string.Empty;
                }
            }
        }

        // メソッド
        /// <summary>ウィンドウプロシージャ
        /// </summary>
        /// <param name="m">ウィンドウメッセージ</param>
        [DebuggerStepThrough]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEWHEEL:
                    try
                    {
                        if (m.WParam.ToInt32() < 0) { this.DownButton(); }
                        else { this.UpButton(); }
                    }
                    catch (OverflowException)
                    {
                        goto default;
                    }
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>Leaveイベントを発生させる。
        /// </summary>
        /// <param name="e">イベントパラメータ</param>
        protected override void OnLeave(EventArgs e)
        {
            decimal n;
            if (!decimal.TryParse(this.Text, out n))
            {
                this.Text = string.Empty;
            }

            base.OnLeave(e);
        }
    }
}

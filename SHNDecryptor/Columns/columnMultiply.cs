﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHNDecrypt
{
    public partial class columnMultiply : Form
    {
        frmMain frmColMulti;
        public columnMultiply(frmMain form)
        {
            frmColMulti = form;
            InitializeComponent();
        }

        private void columnMultiply_Load(object sender, EventArgs e)
        {
            if (frmColMulti.file == null)
            {
                this.Close();
                return;
            }
            init();
        }

        public void init()
        {
            if (frmColMulti.file.table == null) return;
            comboBox1.Items.Clear();
            for (int i = 0; i < frmColMulti.file.table.Columns.Count; i++)
            {
                comboBox1.Items.Add(frmColMulti.file.table.Columns[i].ColumnName);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            int colIndex = frmColMulti.file.getColIndex(comboBox1.Items[comboBox1.SelectedIndex].ToString());
            if (colIndex < 0) return;
            double factor = 0;
            try
            {
                factor = double.Parse(txtFactor.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            string type = frmColMulti.file.table.Columns[colIndex].DataType.ToString();
            if (checkBox1.Checked != true)
            {
                for (int i = 0; i < frmColMulti.file.table.Rows.Count; i++)
                {
                    try
                    {
                        switch (type)
                        {
                            case "System.UInt16":
                                frmColMulti.file.table.Rows[i][colIndex] = (UInt16)((UInt16)frmColMulti.file.table.Rows[i][colIndex] * factor);
                                break;
                            case "System.UInt32":
                                frmColMulti.file.table.Rows[i][colIndex] = (UInt32)((UInt32)frmColMulti.file.table.Rows[i][colIndex] * factor);
                                break;
                            case "System.SByte":
                                frmColMulti.file.table.Rows[i][colIndex] = (SByte)((SByte)frmColMulti.file.table.Rows[i][colIndex] * factor);
                                break;
                            case "System.Byte":
                                frmColMulti.file.table.Rows[i][colIndex] = (Byte)((Byte)frmColMulti.file.table.Rows[i][colIndex] * factor);
                                break;
                            default:
                                frmColMulti.file.table.Rows[i][colIndex] = (int)((int)frmColMulti.file.table.Rows[i][colIndex] * factor);
                                break;
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); break; } //wrong item conv
                }
            }
            else
            {
                if (frmColMulti.dataGrid.SelectedRows.Count != 0)
                {
                    foreach (DataGridViewRow r in frmColMulti.dataGrid.SelectedRows)
                    {
                        try
                        {
                            switch (type)
                            {
                                case "System.UInt16":
                                    frmColMulti.file.table.Rows[r.Index][colIndex] = (UInt16)((UInt16)frmColMulti.file.table.Rows[r.Index][colIndex] * factor);
                                    break;
                                case "System.UInt32":
                                    frmColMulti.file.table.Rows[r.Index][colIndex] = (UInt32)((UInt32)frmColMulti.file.table.Rows[r.Index][colIndex] * factor);
                                    break;
                                case "System.SByte":
                                    frmColMulti.file.table.Rows[r.Index][colIndex] = (SByte)((SByte)frmColMulti.file.table.Rows[r.Index][colIndex] * factor);
                                    break;
                                case "System.Byte":
                                    frmColMulti.file.table.Rows[r.Index][colIndex] = (Byte)((Byte)frmColMulti.file.table.Rows[r.Index][colIndex] * factor);
                                    break;
                                default:
                                    frmColMulti.file.table.Rows[r.Index][colIndex] = (int)((int)frmColMulti.file.table.Rows[r.Index][colIndex] * factor);
                                    break;
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); break; } //wrong item conv
                    }
                }
                else
                {
                    MessageBox.Show("Please select the appropriate rows to modify.");
                    frmColMulti.SQLStatus.Text = "Please select the appropriate rows to modify.";
                }
            }
            //this.Close();
            frmColMulti.SQLStatus.Text = comboBox1.SelectedItem.ToString() + " column has been multiplied by " + factor.ToString() + ".";
        }
    }
}

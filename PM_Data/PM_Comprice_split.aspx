<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_Comprice_split.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Comprice_split" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>外协件拆分</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="box-wrapper">
        <div class="box-outer">
           <div style="height: 470px; overflow: auto; width: 100%">
              <div class="cpbox xscroll">
                 <table id="tab" align="center" class="nowrap cptable fullwidth">
                      <asp:Repeater ID="tbpc_otherpurbill_list_Repeater" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                       <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>批号</strong>
                                        </td>
                                        <td>
                                            <strong>任务号</strong>
                                        </td>
                                        <td>
                                            <strong>设备</strong>
                                        </td>
                                        <td>
                                            <strong>图号</strong>
                                        </td>
                                        <td>
                                            <strong>总序</strong>
                                        </td>
                                        <td>
                                            <strong>外协类别</strong>
                                        </td>
                                        <td>
                                            <strong>名称</strong>
                                        </td>
                                        <td>
                                            <strong>规格</strong>
                                        </td>
                                        <td>
                                            <strong>材质</strong>
                                        </td>
                                        <td>
                                            <strong>数量</strong>
                                        </td>
                                        <td>
                                            <strong>图纸单重(kg)</strong>
                                        </td>
                                        <td>
                                            <strong>图纸总重(kg)</strong>
                                        </td>
                                        <td>
                                            <strong>材料类别</strong>
                                        </td>
                                        <td>
                                            <strong>长度</strong>
                                        </td>
                                        <td>
                                            <strong>宽度</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                        <td>
                                            <strong>下料</strong>
                                        </td>
                                        <td>
                                            <strong>加工</strong>
                                        </td>
                                        <td>
                                            <strong>外形尺寸</strong>
                                        </td>
                                        <td>
                                            <strong>入库类别</strong>
                                        </td>
                                    </tr>
                                        </HeaderTemplate>
                                         <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="MS_ID" runat="server" Text='<%#Eval("MS_ID")%>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        onclick="checkme(this)" Checked="false"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_PID" runat="server" Text='<%#Eval("MS_PID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                   <asp:Label ID="MS_ENGID" runat="server" Text='<%#Eval("MS_ENGID") %>'></asp:Label>
                                                     <asp:Label ID="MS_PJID" runat="server" Text='<%#Eval("MS_PJID")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_ENGNAME" runat="server" Text='<%#Eval("MS_ENGNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_TUHAO" runat="server" Text='<%#Eval("MS_TUHAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_ZONGXU" runat="server" Text='<%#Eval("MS_ZONGXU")%>'></asp:Label>
                                                </td>
                                                <td>
                                                <asp:Label ID="MS_wxtype" runat="server" Text='<%#Eval("MS_wxtype") %>' ></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_NAME" runat="server" Text='<%#Eval("MS_NAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_GUIGE" runat="server" Text='<%#Eval("MS_GUIGE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_CAIZHI" runat="server" Text='<%#Eval("MS_CAIZHI")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_NUM" runat="server" Text='<%#Eval("MS_NUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_TUWGHT" runat="server" Text='<%#Eval("MS_TUWGHT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_TUTOTALWGHT" runat="server" Text='<%#Eval("MS_TUTOTALWGHT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_MASHAPE" runat="server" Text='<%#Eval("MS_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_LEN" runat="server" Text='<%#Eval("MS_LEN")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_WIDTH" runat="server" Text='<%#Eval("MS_WIDTH")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_NOTE" runat="server" Text='<%#Eval("MS_NOTE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_XIALIAO" runat="server" Text='<%#Eval("MS_XIALIAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_PROCESS" runat="server" Text='<%#Eval("MS_PROCESS")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_WAIXINGCHICUN" runat="server" Text='<%#Eval("MS_WAIXINGCHICUN")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_KU" runat="server" Text='<%#Eval("MS_KU")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                          </div>
                          </div>
                      <table id="tab_split" align="center" class="nowrap cptable fullwidth" visible="false">
                          <asp:Repeater ID="repeater_split" runat="server" >
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                       <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                        <strong style="color:Red">标识号</strong>
                                        </td>
                                        <td>
                                            <strong>批号</strong>
                                        </td>
                                        <td>
                                            <strong>任务号</strong>
                                        </td>
                                        <td>
                                            <strong>设备</strong>
                                        </td>
                                        <td>
                                            <strong>图号</strong>
                                        </td>
                                        <td>
                                            <strong>总序</strong>
                                        </td>
                                        <td>
                                            <strong>外协类别</strong>
                                        </td>
                                        <td>
                                            <strong>名称</strong>
                                        </td>
                                        <td>
                                            <strong>规格</strong>
                                        </td>
                                        <td>
                                            <strong>材质</strong>
                                        </td>
                                        <td>
                                            <strong>数量</strong>
                                        </td>
                                        <td>
                                            <strong>图纸单重(kg)</strong>
                                        </td>
                                        <td>
                                            <strong>图纸总重(kg)</strong>
                                        </td>
                                        <td>
                                            <strong>材料类别</strong>
                                        </td>
                                        <td>
                                            <strong>长度</strong>
                                        </td>
                                        <td>
                                            <strong>宽度</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                        <td>
                                            <strong>下料</strong>
                                        </td>
                                        <td>
                                            <strong>加工</strong>
                                        </td>
                                        <td>
                                            <strong>外形尺寸</strong>
                                        </td>
                                        <td>
                                            <strong>入库类别</strong>
                                        </td>
                                    </tr>
                                        </HeaderTemplate>
                                         <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="MS_ID" runat="server" Text='<%#Eval("MS_ID")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                <asp:Label ID="MS_CFID" runat="server" Text='<%#Eval("MS_CFID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_PID" runat="server" Text='<%#Eval("MS_PID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                   <asp:Label ID="MS_ENGID" runat="server" Text='<%#Eval("MS_ENGID") %>'></asp:Label>
                                                     <asp:Label ID="MS_PJID" runat="server" Text='<%#Eval("MS_PJID")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_ENGNAME" runat="server" Text='<%#Eval("MS_ENGNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_TUHAO" runat="server" Text='<%#Eval("MS_TUHAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_ZONGXU" runat="server" Text='<%#Eval("MS_ZONGXU")%>'></asp:Label>
                                                </td>
                                                <td>
                                                <asp:Label ID="MS_wxtype" runat="server" Text='<%#Eval("MS_wxtype") %>' ></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_NAME" runat="server" Text='<%#Eval("MS_NAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_GUIGE" runat="server" Text='<%#Eval("MS_GUIGE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_CAIZHI" runat="server" Text='<%#Eval("MS_CAIZHI")%>'></asp:Label>
                                                </td>
                                                <td>
                                                <asp:TextBox ID="txt_num" runat="server" Text=""></asp:TextBox>
                                                    <asp:Label ID="MS_NUM" runat="server" Text='<%#Eval("MS_NUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_TUWGHT" runat="server" Text='<%#Eval("MS_TUWGHT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_TUTOTALWGHT" runat="server" Text='<%#Eval("MS_TUTOTALWGHT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_MASHAPE" runat="server" Text='<%#Eval("MS_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_LEN" runat="server" Text='<%#Eval("MS_LEN")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_WIDTH" runat="server" Text='<%#Eval("MS_WIDTH")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_NOTE" runat="server" Text='<%#Eval("MS_NOTE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_XIALIAO" runat="server" Text='<%#Eval("MS_XIALIAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_PROCESS" runat="server" Text='<%#Eval("MS_PROCESS")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_WAIXINGCHICUN" runat="server" Text='<%#Eval("MS_WAIXINGCHICUN")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_KU" runat="server" Text='<%#Eval("MS_KU")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
             </div>
   </div>
                          
    </form>
</body>
</html>

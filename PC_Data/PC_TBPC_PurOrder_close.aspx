<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_TBPC_PurOrder_close.aspx.cs"
    Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_PurOrder_close"  Title="订单关闭"   MasterPageFile="~/Masters/PopupBase.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
<link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <script language="JavaScript" type="text/javascript">
    function openclosemodewin1()
    {
       var orderno="<%=globlempcode%>";
       var autonum=Math.round(10000*Math.random());
       window.open("PC_TBPC_marstouseallGB.aspx?autonum="+autonum+"&pcode="+escape(orderno)+"");
    }
     function openclosemodewin()
    {
       var orderno="<%=globlempcode%>";
       var autonum=Math.round(10000*Math.random());
       window.open("PC_TBPC_Marreplace_edit.aspx?autonum="+autonum+"&mpcode="+escape(orderno)+"");
    }
    </script>
    <div >
            <asp:Label ID="ControlFinder" runat="server" Text="" Visible="false"></asp:Label>
        <table width="100%">
            <tr>
               <td width="100%">
                  注：红色代表关闭，黄色代表关闭后进行了占用处理，绿色代表关闭后进行了相似代用处理！
               </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btn_ZY" runat="server" Text="占用库存" onclick="btn_ZY_Click"/>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_XSDY" runat="server" Text="相似代用" onclick="btn_XSDY_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_close" runat="server" Text="入库关闭"  OnClick="btn_close_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_confirm" runat="server" Text="意外关闭" OnClick="btn_confirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_concel" runat="server" Text="反关闭" OnClick="btn_concel_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                     <asp:RadioButton ID="rad_stall" runat="server" Text="全部" TextAlign="Right" GroupName="select11"
                             OnCheckedChanged="rad_stall_CheckedChanged" AutoPostBack="true"  Checked="true"/>
                      <asp:RadioButton ID="rad_guanbi" runat="server" Text="关闭" TextAlign="Right" GroupName="select11"
                                            OnCheckedChanged="rad_guanbi_CheckedChanged" AutoPostBack="true"  /> 
                      <asp:RadioButton ID="rad_zc" runat="server" Text="正常" TextAlign="Right" GroupName="select11"
                                            OnCheckedChanged="rad_zc_CheckedChanged" AutoPostBack="true"  />      
                </td>
            </tr>
        </table>
        <table width="100%" style="text-align: center;">
            <tr align="center">
                <td style="font-size: x-large; text-align: center;" colspan="4">
                    采购订单(关闭)
                </td>
                <td >
                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                        Visible="false" />
                </td>
            </tr>
            <tr align="center">
                <td style="width: 70%;" align="left">
                    &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:Label ID="LabelDate" runat="server"></asp:Label>
                </td>
                <td style="width: 30%;" align="left">
                    &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td style="width: 70%;" align="left">
                    &nbsp;&nbsp;&nbsp;供应商：<asp:Label ID="LabelSupplier" runat="server"></asp:Label>
                </td>
                <td style="width: 30%;" align="left">
                    &nbsp;&nbsp;&nbsp;摘&nbsp;&nbsp;&nbsp;要：<asp:Label ID="LabelAbstract" runat="server"
                        Visible="false"></asp:Label>
                </td>
            </tr>
            <tr id="ws" visible="false" runat="server">
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;版本号：<asp:Label ID="LabelVersionNo" runat="server" Visible="false"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    变更日期：<asp:Label ID="LabelChangeDate" runat="server" Visible="false"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;变更人：<asp:Label ID="LabelChangeMan" runat="server" Visible="false"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    变更原因：<asp:Label ID="LabelChangeReason" runat="server" Visible="false"></asp:Label>
                    <asp:TextBox ID="tb_state" runat="server" Text="" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="tb_cstate" runat="server" Text="" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="height: 500px; overflow: auto; width: 100%;">
            <table width="100%">
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" EmptyDataText="没有记录!"  OnRowDataBound="GridView1_RowDataBound">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" CssClass="checkBoxCss" />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="计划跟踪号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="PlanCode" runat="server"  Text='<%# Eval("PlanCode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="材料编码" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="MaterialCode" runat="server"  Text='<%# Eval("MaterialCode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="MaterialName" HeaderText="名称" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MaterialStandard" HeaderText="标准" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MaterialTexture" HeaderText="材质" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MaterialGb" HeaderText="国标" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Unit" HeaderText="单位" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="UnitPrice" DataFormatString="{0:f}"  HeaderText="价格" ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Number" runat="server"  Text='<%# Eval("Number") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="辅助数量" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="fznum" runat="server"  Text='<%# Eval("fznum") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="zxnum" HeaderText="执行数量" ItemStyle-HorizontalAlign="Center" />
                                
                                <asp:TemplateField HeaderText="长度" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Length" runat="server"  Text='<%# Eval("Length") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="宽度" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Width" runat="server"  Text='<%# Eval("Width") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="已到货数量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_arry" runat="server" Text='<%# Eval("arrivedNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="原因">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tb_value" runat="server" Text='<%# Eval("Comment") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="审核/到货/关闭标识">
                                    <ItemTemplate>
                                        <asp:Label ID="statetext" runat="server" Text='<%#get_order_state(Eval("PO_STATE").ToString(),Eval("pocstate").ToString())%>'></asp:Label>
                                        <asp:Label ID="state" runat="server" Text='<%# Eval("PO_STATE") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="cstate" runat="server" Text='<%# Eval("pocstate") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                             <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />  
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%">
            <tr>
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;主&nbsp;&nbsp;&nbsp;管：<asp:Label ID="LabelManager" runat="server"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：<asp:Label ID="LabelDep" runat="server"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;业务员：<asp:Label ID="LabelClerk" runat="server"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDocument" runat="server"></asp:Label>
                    <asp:Label ID="LabelDocumentid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </asp:Content>


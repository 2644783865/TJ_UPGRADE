<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_wxInvoice_Managemnt.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_wxInvoice_Managemnt" Title="无标题页" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    外协发票管理
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    
    <script language="javascript" type="text/javascript">
     function SearchUp(ObjUniqCode) {
        window.open("../FM_Data/FM_JSD_Info.aspx?UniqId=" + ObjUniqCode);
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
 </cc1:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                       <td align="left" style="width: 6%;">
                        是否审核:
                        </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rblSFSH" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblSFSH_SelectedIndexChanged">
                            <asp:ListItem Text="全部" Value="%" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                            <asp:ListItem Text="否" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="left" style="width: 6%;">
                        是否勾稽:
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rblSFGJ" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblSFGJ_SelectedIndexChanged">
                            <asp:ListItem Text="全部" Value="%" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                            <asp:ListItem Text="否" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    
                      <td align="right">
                        <asp:Button ID="btnSc" runat="server" Text="上查" OnClick="btnSc_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnToRKD" runat="server" Text="到结算单" OnClick="btnToRKD_Click" />&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="Button1" runat="server" ForeColor="red" BorderStyle="Solid" Text="删除" OnClientClick="javascript:return confirm('确定要删除吗？删除以后结算单进入初始状态，可以重新生成发票！');" onclick="btndelete_Click"  />
                         </td>&nbsp;&nbsp;&nbsp;
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <table width="100%">
               
                <tr>
                    <td align="right">
                        <strong>日期:</strong>
                    </td>
                    <td>
                        从<asp:TextBox ID="txtStartYearMonth" runat="server" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>到
                        <asp:TextBox ID="txtEndYearMonth" runat="server" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    发票号：
                        <asp:TextBox ID="txtfpCode" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                     凭证号：<asp:TextBox ID="txtpzh" runat="server">
                    </asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                        供应商:
                    &nbsp;&nbsp;&nbsp;
                     <asp:TextBox ID="tb_supply" runat="server"></asp:TextBox>
                     <asp:Button ID="btnCx" runat="server" Text="查 询" OnClick="btnCx_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                     </td>
                   
                   
                </tr>
            </table>
            <asp:HiddenField ID="hfdTotalJE" runat="server" />
            <asp:HiddenField ID="hfdTotalSE" runat="server" />
            <asp:HiddenField ID="hfdTotalHSJE" runat="server" />
          
                <yyc:SmartGridView ID="grvInv" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" ShowFooter="true" OnRowDataBound="grvInv_RowDataBound">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                            <asp:CheckBox ID="checkbox" runat="server" />
                                <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="发票编号" DataField="WXGI_CODE" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="供应商" DataField="WXGI_GYSNAME" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />                      
                        <asp:BoundField HeaderText="发票号码" DataField="WXGI_FPNUM" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="金额" DataField="WXGI_MONEY" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="税额" DataField="WXGI_SE" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="含税金额" DataField="WXGI_HSMONEY"
                            HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="凭证号" DataField="WXGI_PZH" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="记账人" DataField="WXGI_JZNAME" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="制单人" DataField="WXGI_ZDNAME" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="登记日期" DataField="WXGI_DATE" DataFormatString="{0:d}" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        <asp:TemplateField HeaderText="是否审核" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox12" runat="server" Checked='<%#Eval("WXGI_STATE").ToString()=="0"||Eval("WXGI_STATE").ToString()==""?false:true %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否勾稽" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox13" runat="server" Checked='<%#Eval("WXGI_GJFLAG").ToString()=="0"||Eval("WXGI_GJFLAG").ToString()==""?false:true %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否核算" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox14" runat="server" Checked='<%#Eval("WXGJ_HSSTATE").ToString()=="0"||Eval("WXGJ_HSSTATE").ToString()==""?false:true %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审核" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlContrac1t" Target="_blank" ToolTip="审核" NavigateUrl='<%#"~/FM_Data/FM_wxInvoice.aspx?Action=Audit&WXGI_CODE="+Eval("WXGI_CODE") %>'
                                    Visible='<%#Eval("WXGI_STATE").ToString()=="0"?true:false %>' CssClass="link" runat="server">
                                    <asp:Image ID="Image1544" ImageUrl="~/Assets/images/res.gif" runat="server" />审核
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="钩稽" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlGJ"  Target="_blank" ToolTip="钩稽" NavigateUrl='<%#"~/FM_Data/FM_wxInvoice.aspx?Action=Trick&WXGI_CODE="+Eval("WXGI_CODE") %>'
                                    Visible='<%#Eval("WXGI_STATE").ToString()=="0"?false:Eval("WXGI_GJFLAG").ToString()=="0"?true:false %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image1232" ImageUrl="~/Assets/images/res.gif" runat="server" />钩稽
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="反钩稽" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlFGJ" Target="_blank" ToolTip="反钩稽" NavigateUrl='<%#"~/FM_Data/FM_wxInvoice.aspx?Action=TrickReject&WXGI_CODE="+Eval("WXGI_CODE") %>'
                                    Visible='<%#Eval("WXGI_GJFLAG").ToString()=="0"?false:Eval("WXGJ_HSSTATE").ToString()=="0"?true:false %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image121" ImageUrl="~/Assets/images/res.gif" runat="server" />反钩稽
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlCK" Target="_blank" ToolTip="查看" NavigateUrl='<%#"~/FM_Data/FM_wxInvoice.aspx?Action=View&WXGI_CODE="+Eval("WXGI_CODE") %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                    查看
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                    <FixRowColumn FixRowType="Header" TableWidth="99%" TableHeight="380px" />
                </yyc:SmartGridView>
         
            <asp:Panel ID="NoDataPanel" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                没有记录！
            </asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>

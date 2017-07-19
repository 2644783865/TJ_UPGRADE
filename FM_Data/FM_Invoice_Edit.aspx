<%@ Page Title="购货发票" Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="FM_Invoice_Edit.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_Invoice_Edit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    
    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
    <asp:Panel ID="Operation" runat="server">
    <table width="100%">
    <tr>
        <td>
            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
        </td>
        <td align="right">
        <asp:Button ID="save" runat="server" Text="保存" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="submit" runat="server" Text="提交" CausesValidation="true" />&nbsp;&nbsp;&nbsp;        
        <asp:Button ID="goback" runat="server" Text="返回"  CausesValidation="false" />&nbsp;&nbsp;&nbsp;                        
        </td>
    </tr>
    </table>
    </asp:Panel>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper" >
    <asp:Panel ID="HeadPanel" runat="server" Width="100%">
    <table width="100%">
        <tr>
            <td style=" font-size:x-large; text-align:center;" colspan="3">
                购货发票（专业）<asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false"/>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>打印次数：<asp:Label ID="LabelPrintTimes" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;供应商：<asp:DropDownList ID="DropDownListSupplier" runat="server">
                </asp:DropDownList>
            </td>    
            <td>
                开户银行：<asp:Label ID="LabelBank" runat="server"></asp:Label>
            </td>
            <td>
                发票号码：<asp:Label ID="LabelInvoiceNo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;地&nbsp;&nbsp;&nbsp;址：<asp:Label ID="LabelAddress" runat="server"></asp:Label>            
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate" Format="yyyy-MM-dd">
                </cc1:CalendarExtender>            
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;凭证号：<asp:Label ID="LabelCertificateCode" runat="server"></asp:Label>            
            </td>            
        </tr>
    </table>
    </asp:Panel>
    

    <asp:Panel ID="PanelBody" runat="server"  style="overflow:auto;height: 420px; width: 100%; position:static">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" EmptyDataText="没有相关数据！" ShowFooter="False">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="物料代码">
                <ItemTemplate>
                    <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="物料名称">
                <ItemTemplate>
                    <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>        
            <asp:TemplateField HeaderText="规格型号">
                <ItemTemplate>
                    <asp:Label ID="TextBoxMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>          
            <asp:TemplateField HeaderText="单位">
                <ItemTemplate>
                    <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="数量">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Number")%>'></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>    
            <asp:TemplateField HeaderText="单价">
                <ItemTemplate>
                    <asp:Label ID="LabelUnitPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UnitPrice")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="含税单价">
                <ItemTemplate>
                    <asp:Label ID="LabelCTUP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CTUP")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="金额">
                <ItemTemplate>
                    <asp:Label ID="LabelAmount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Amount")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="税率">
                <ItemTemplate>
                    <asp:Label ID="LabelTaxRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TaxRate")%>'></asp:Label>%
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="税额">
                <ItemTemplate>
                    <asp:Label ID="LabelTax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Tax")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="价税合计">
                <ItemTemplate>
                    <asp:Label ID="LabelAmountAndTax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountAndTax")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="价税合计（本位币）">
                <ItemTemplate>
                    <asp:Label ID="LabelTaxAndAmountCurreny" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TaxAndAmountCurreny")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>                     
            <asp:TemplateField HeaderText="计划模式">
                <ItemTemplate>
                    <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划跟踪号">
                <ItemTemplate>
                    <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
        </Columns>
    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
    <RowStyle BackColor="#EFF3FB"/>
    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </asp:Panel>
    
    <asp:Panel ID="FooterPanel" runat="server" Width="100%">
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：<asp:DropDownList ID="DropDownListDep" runat="server">
                </asp:DropDownList>
            </td>
            <td align="left">
                 &nbsp;&nbsp;&nbsp;记&nbsp;&nbsp;&nbsp;账：<asp:DropDownList ID="DropDownListAccount" runat="server">
                </asp:DropDownList>
            </td>
            <td align="left">
                &nbsp;&nbsp;&nbsp;制 &nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server" Enabled="false"></asp:Label>
                <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
            </td>
       </tr>
        <tr>
            <td colspan="3">
            </td>
        </tr>
    </table>
    </asp:Panel>

    </div>



</asp:Content>

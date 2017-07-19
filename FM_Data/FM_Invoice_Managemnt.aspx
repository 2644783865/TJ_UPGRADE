<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="FM_Invoice_Managemnt.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_Invoice_Managemnt" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    发票管理
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    
    <script language="javascript" type="text/javascript">
     function SearchUp(id) {
        var date=new Date();
        var time=date.getTime();
        window.open("../SM_Data/SM_WarehouseIn_Manage.aspx?action=SearchUpOrDown&id="+id+"&time="+time);
    }
    
    </script>
    
 <style type="text/css"> 
     .autocomplete_completionListElement 
     {  
     	margin : 0px; 
     	background-color : #1C86EE; 
     	color : windowtext; 
     	cursor : 'default'; 
     	text-align : left; 
     	list-style:none; 
     	padding:0px;
        border: solid 1px gray; 
        width:400px!important;   
     }
     .autocomplete_listItem 
     {   
     	border-style : solid; 
     	border :#FFEFDB; 
     	border-width : 1px;  
     	background-color : #EEDC82; 
     	color : windowtext;  
     } 
     .autocomplete_highlightedListItem 
     { 
     	background-color: #1C86EE; 
     	color: black; 
     	padding: 1px; 
     } 
  </style> 
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
                        是否审核：
                        </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rblSSSH" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblSSGJ_SelectedIndexChanged">
                            <asp:ListItem Text="全部" Value="%" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                            <asp:ListItem Text="否" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="left" style="width: 6%;">
                        是否勾稽:
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rblSSGJ" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblSSGJ_SelectedIndexChanged">
                            <asp:ListItem Text="全部" Value="%" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                            <asp:ListItem Text="否" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    
                      
                        <td align="right">
                        <asp:Button ID="btnSC" runat="server" Text="上查" OnClick="btnSC_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnRKD" runat="server" Text="到入库单" OnClick="btnRKD_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btngjlxcx" runat="server" Text="发票勾稽物料类型查询" OnClick="btngjlxcx_Click" />&nbsp;&nbsp;&nbsp;
                    </td>
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
                    <td style="width:auto">
                        从<asp:TextBox ID="txtStartTime" class="easyui-datebox" runat="server" onClick="setday(this)"></asp:TextBox>
                        到
                        <asp:TextBox ID="txtEndTime" runat="server" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>  
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;发票号：
                        <asp:TextBox ID="txtfpCode" runat="server"></asp:TextBox>
                    
                     <asp:TextBox ID="txtCODE" runat="server" Visible="false"></asp:TextBox>
                   
                   
                     凭证号：&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtpzh" runat="server">
                    </asp:TextBox>
                        供应商:
                    &nbsp;&nbsp;&nbsp;
                     <asp:TextBox ID="tb_supply" runat="server" OnTextChanged="tb_supply_Textchanged" AutoPostBack="true"></asp:TextBox>
                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_supply"
                                            ServicePath="../PC_Data/PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                                            ServiceMethod="GetCusupinfo" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                                        </cc1:AutoCompleteExtender>
                      &nbsp;&nbsp;&nbsp;
                       <asp:Button ID="btnQuery" runat="server" Text="查 询" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:Button ID="btnAllData" runat="server" Text="全 部" OnClick="btnAllData_Click"
                            Visible="false" />
                       </td>
                </tr>
            </table>
            <asp:HiddenField ID="hfdTotalAmount" runat="server" />
            <asp:HiddenField ID="hfdTotalSE" runat="server" />
            <asp:HiddenField ID="hfdTotalCTAmount" runat="server" />
          
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
                        <asp:BoundField HeaderText="编号" DataField="GI_CODE" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="供应商" DataField="GI_SUPPLIERNM" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />                      
                        <asp:BoundField HeaderText="发票号码" DataField="GI_INVOICENO" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="金额" DataField="GI_AMTMNY" DataFormatString="{0:c}" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="税额" DataField="GI_SE" HeaderStyle-Wrap="false" DataFormatString="{0:c}"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="税总金额" DataField="GI_CTAMTMNY" DataFormatString="{0:c}"
                            HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="凭证号" DataField="GI_PZH" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="记账人" DataField="GI_JZNM" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="制单人" DataField="GI_ZDNM" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="登记日期" DataField="GI_DATE" DataFormatString="{0:d}" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        <asp:TemplateField HeaderText="是否审核" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox12" runat="server" Checked='<%#Eval("GI_STATE").ToString()=="0"?false:true %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否勾稽" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox13" runat="server" Checked='<%#Eval("GI_GJFLAG").ToString()=="0"?false:true %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否核算" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox14" runat="server" Checked='<%#Eval("GJ_HSSTATE").ToString()=="0"?false:true %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审核" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlContrac1t" Target="_blank" ToolTip="审核" NavigateUrl='<%#"~/FM_Data/FM_Invoice.aspx?Action=Audit&GI_CODE="+Eval("GI_CODE") %>'
                                    Visible='<%#Eval("GI_STATE").ToString()=="0"?true:false %>' CssClass="link" runat="server">
                                    <asp:Image ID="Image1544" ImageUrl="~/Assets/images/res.gif" runat="server" />审核
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="钩稽" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlContract11"  Target="_blank" ToolTip="钩稽" NavigateUrl='<%#"~/FM_Data/FM_Invoice.aspx?Action=Trick&GI_CODE="+Eval("GI_CODE") %>'
                                    Visible='<%#Eval("GI_STATE").ToString()=="0"?false:Eval("GI_GJFLAG").ToString()=="0"?true:false %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image1232" ImageUrl="~/Assets/images/res.gif" runat="server" />钩稽
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="反钩稽" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlContract1" Target="_blank" ToolTip="反钩稽" NavigateUrl='<%#"~/FM_Data/FM_Invoice.aspx?Action=TrickOff&GI_CODE="+Eval("GI_CODE") %>'
                                    Visible='<%#Eval("GI_GJFLAG").ToString()=="0"?false:Eval("GJ_HSSTATE").ToString()=="0"?true:false %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image121" ImageUrl="~/Assets/images/res.gif" runat="server" />反钩稽
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlContract3" Target="_blank" ToolTip="查看" NavigateUrl='<%#"~/FM_Data/FM_Invoice.aspx?Action=View&GI_CODE="+Eval("GI_CODE") %>'
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
         
            <asp:Panel ID="NoDataPanel" runat="server" Visible="false" HorizontalAlign="Center">
                没有记录！
            </asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>

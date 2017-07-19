<%@ Page Title="物料需用计划执行情况查询" Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master"
    AutoEventWireup="true" CodeBehind="SM_Warehouse_MaterialFlow.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_MaterialFlow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>
    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    
    <script language="javascript" type="text/javascript">
    
       function ShowInModal(eng,mar) {
    
        window.open("SM_Warehouse_MaterialFlow_INDetail.aspx?FLAG=ZX&&eng=" +eng+"&&mar="+mar);

        }
       function ShowOutModal(eng,mar) {
    
        window.open("SM_Warehouse_MaterialFlow_OUTDetail.aspx?FLAG=ZX&&eng=" +eng+"&&mar="+mar);

        }
        
     </script>

    
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Panel ID="Operation" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="true"></asp:Label>
                            </td>
                            <td align="right">
                            <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="HeadPanel" runat="server" Width="100%">
                <table width="100%">
                    <tr>
                        <td style="font-size: x-large; text-align: center;" colspan="5">
                            物料执行情况查询
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 20%" align="left">
                            生产制号:<asp:TextBox ID="TextBoxSCZH" runat="server" OnTextChanged="TextBoxSCZH_TextChanged"
                                AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="ac1" runat="server" TargetControlID="TextBoxSCZH" FirstRowSelected="true"
                                MinimumPrefixLength="1" ServiceMethod="getEngID" ServicePath="~/Ajax.asmx" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                CompletionSetCount="30">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td style="width: 20%" align="left">
                            物料编码:<asp:TextBox ID="TextBoxMar" runat="server" OnTextChanged="TextBoxMar_TextChanged"
                                AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode"
                                ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="TextBoxMar"
                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td style="width: 20%" align="left">
                            物料名称:<asp:TextBox ID="TextBoxMarNM" runat="server" OnTextChanged="TextBoxSCZH_TextChanged"
                                AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                        </td>
                        <td style="width: 20%" align="left">
                            物料规格:<asp:TextBox ID="TextBoxGG" runat="server" OnTextChanged="TextBoxSCZH_TextChanged"
                                AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                        </td>
                         <td style="width: 20%" align="left">
                             <asp:DropDownList ID="DropDownListDifnum" runat="server" 
                                 onselectedindexchanged="DropDownListDifnum_SelectedIndexChanged" AutoPostBack="true">
                             <asp:ListItem Value="0">-请选择-</asp:ListItem>
                             <asp:ListItem Value="3">未支领</asp:ListItem>
                             <asp:ListItem Value="1">正常</asp:ListItem>
                             <asp:ListItem Value="2">超领</asp:ListItem>
                             <asp:ListItem Value="4">未支领-正常</asp:ListItem>
                             <asp:ListItem Value="5">未支领-超领</asp:ListItem>
                             <asp:ListItem Value="6">正常-超领</asp:ListItem>
                             </asp:DropDownList>                     
                      </td>
                    </tr>
                    <tr>
                    <td style="width: 20%" align="left">  
                        材质：<asp:TextBox ID="TextBoxCZ" runat="server" AutoPostBack="true" 
                            onclick="this.select();" ontextchanged="TextBoxCZ_TextChanged"></asp:TextBox>
                    </td>
                    <td style="width: 20%" align="left">  </td>
                    <td style="width: 20%" align="left">  </td>
                     <td style="width: 20%" align="right">
                      
                     <asp:RadioButtonList ID="RadioButtonList1" runat="server"  AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                         <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                         <asp:ListItem Text="已备注" Value="1"></asp:ListItem>
                         <asp:ListItem Text="未备注" Value="2" Selected="True"></asp:ListItem>                     
                     </asp:RadioButtonList>
                     
                     </td>
                     <td style="width: 20%" align="right">                     
                      <asp:Button runat="server" ID="Button1" OnClick="SaveNote_Click" Text="保存备注" />
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    
                    </tr>
                </table>
            </asp:Panel>
            <div style="overflow: auto; width: 100%; height: 400px;">
                <asp:HiddenField ID="HiddenFieldNum" runat="server" />
                <asp:HiddenField ID="HiddenFieldOrderNum" runat="server" />
                <asp:HiddenField ID="HiddenFieldInNum" runat="server" />
                <asp:HiddenField ID="HiddenFieldOutNum" runat="server" />
                <asp:HiddenField ID="HiddenFieldDifNum" runat="server" />
            
                <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                    没有相关记录!</asp:Panel>
                <yyc:SmartGridView ID="GridView1" Width="100%"  runat="server" AllowSorting="true" OnSorting="GridView1_Sorting"
                    AutoGenerateColumns="False" ForeColor="#333333" DataKeyNames="marid,RP_TSAID,engid" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound"  EmptyDataText="没有相关数据">
                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                                 <asp:Label runat="server" ID="Labelengid" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RP_TSAID" HeaderText="生产制号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" SortExpression="RP_TSAID"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="marid" HeaderText="物料代码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" 
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="marnm" HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" SortExpression="marnm"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="margg" HeaderText="规格型号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" SortExpression="margg" HtmlEncode="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="marcz" HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" SortExpression="marcz" HtmlEncode="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="margb" HeaderText="国标" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HtmlEncode="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="marunit" HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="num" HeaderText="计划数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ordernum" HeaderText="订单数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="innum" HeaderText="入库数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" /> 
                        <asp:BoundField DataField="outnum" HeaderText="出库数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="difnum" HeaderText="差额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" FooterStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="领用计划" HeaderStyle-Wrap="false">
                               <ItemTemplate>
                                <asp:TextBox runat="server" ID="TextBoxLingYong" Text='<%#DataBinder.Eval(Container.DataItem, "LingYong")%>' Width="80px"></asp:TextBox>
                               </ItemTemplate>
                               <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                               <ItemTemplate>
                                <asp:TextBox runat="server" ID="TextBoxNote" Text='<%#DataBinder.Eval(Container.DataItem, "Note")%>'></asp:TextBox>
                               </ItemTemplate>
                               <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注时间" HeaderStyle-Wrap="false">
                               <ItemTemplate>
                                <asp:TextBox runat="server" ID="TextBoxDate" Text='<%#DataBinder.Eval(Container.DataItem, "NoteDate")%>'></asp:TextBox>
                                  
                               </ItemTemplate>
                               <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                       </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FixRowColumn FixRowType="Header" />
                </yyc:SmartGridView>
                
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    
    <script type="text/javascript">
  
   var sDataTable=document.getElementById('<%=GridView1.ClientID %>')
  
   function RowClick()
   {
           for (var i=1, j=sDataTable.tBodies[0].rows.length-1; i<j; i++) 
          {
            sDataTable.tBodies[0].rows[i].onclick= function (i) 
            {
                var clicked = false;
                var dataRow = sDataTable.tBodies[0].rows[i];
                return function () 
                      {
                            if (clicked) 
                            {
                                dataRow.style.backgroundColor = "#ffffff";
                              
                                clicked = false;
                            }
                            else 
                            {
                                dataRow.style.backgroundColor = "#A8B7EC";
                                clicked = true;
                            }
                        }
             }.call(this, i);
           }
   }

   RowClick();

  </script>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="FM_RuKu_Adjust_Accounts_View.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_RuKu_Adjust_Accounts_View"
    Title="无标题页" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:content id="Content1" contentplaceholderid="PrimaryContent" runat="server">
    <link href="StyleFile/model.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
   function cancel() 
   {
     window.parent.document.getElementById('ButtonEditCancel').click();
   }
    </script>

    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    <div class="popup_Container">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <div class="TitlebarRight" onclick="cancel();">
                    </div>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div class="popup_Body">
                    <div style="font-size: x-large; font-weight: bold; color: #000000;">
                        发票信息</div>
                    <div style="height: 200px;">
                        <yyc:SmartGridView ID="GridViewInvoice" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="GridViewInvoice_RowDataBound" ShowFooter="true">
                            <Columns>
                                <asp:templatefield headertext="物料编码">
                                    <itemtemplate>
                                        <asp:Label ID="LabelMarID" runat="server" Text='<%# Bind("GI_MATCODE") %>'></asp:Label>
                                    </itemtemplate>
                                    <footertemplate>
                                        合计:
                                    </footertemplate>
                                    <footerstyle horizontalalign="Center" verticalalign="Middle" />
                                </asp:templatefield>
                                <asp:boundfield datafield="GI_NAME" headertext="物料名称" />
                                <asp:boundfield datafield="GI_GUIGE" headertext="规格" />
                                <asp:boundfield datafield="GI_UNIT" headertext="单位" />
                                <asp:boundfield datafield="GI_NUM" headertext="数量" />
                                <asp:templatefield headertext="金额">
                                    <itemtemplate>
                                        <asp:Label ID="LabelAmount" runat="server" Text='<%# Bind("GI_AMTMNY") %>'></asp:Label>
                                    </itemtemplate>
                                    <footertemplate>
                                        <asp:Label ID="TotalAmount" runat="server" ></asp:Label>
                                    </footertemplate>
                                </asp:templatefield>
                                <asp:boundfield datafield="GI_TAXRATE" headertext="税率" />
                                <asp:templatefield headertext="含税金额">
                                    <itemtemplate>
                                        <asp:Label ID="LabelCTAmount" runat="server" Text='<%# Bind("GI_CTAMTMNY") %>'></asp:Label>
                                    </itemtemplate>
                                    <footertemplate>
                                        <asp:Label ID="TotalCTAmount" runat="server" ></asp:Label>
                                    </footertemplate>
                                </asp:templatefield>
                            </Columns>
                            <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <RowStyle BackColor="#ffffff" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <FixRowColumn FixRowType="Header,Pager" />
                        </yyc:SmartGridView>

                        <script language="javascript" type="text/javascript">
 
   var  table=document.getElementById("<%=GridViewInvoice.ClientID %>");
   function RowClick()
   {
           for (var i=1, j=table.tBodies[0].rows.length-1; i<j; i++) 
          {
            table.rows[i].onclick= function (i) 
            {
                var clicked = false;
                var dataRow = table.tBodies[0].rows[i];
                return function () 
                      {
                            if (clicked) 
                            {
                                dataRow.style.backgroundColor = "#ffffff";
                                clicked = false;
                            }
                            else 
                            {
                                dataRow.style.backgroundColor = "#BFDFFF";
                                clicked = true;
                            }
                        }
             }.call(this, i);
           }
   }
   RowClick(); 
                        </script>

                    </div>
                    <div style="font-size: x-large; font-weight: bold; color: #000000;">
                        入库单信息</div>
                    <div style="height: 200px;">
                        <yyc:SmartGridView ID="GridViewIn" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewIn_RowDataBound"
                            ShowFooter="true">
                            <Columns>
                                <asp:boundfield datafield="GI_MATCODE" headertext="物料编码" />
                                <asp:boundfield datafield="GI_NAME" headertext="物料名称" />
                                <asp:boundfield datafield="GI_GUIGE" headertext="规格" />
                                <asp:boundfield datafield="GI_UNIT" headertext="单位" />
                                <asp:boundfield datafield="GI_NUM" headertext="数量" />
                                <asp:boundfield datafield="GI_AMTMNY" headertext="金额" />
                                <asp:boundfield datafield="GI_TAXRATE" headertext="税率" />
                                <asp:boundfield datafield="GI_CTAMTMNY" headertext="含税金额" />
                            </Columns>
                            <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <RowStyle BackColor="#ffffff" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <FixRowColumn FixRowType="Header,Pager" />
                        </yyc:SmartGridView>

                        <script language="javascript" type="text/javascript">
 
   var  tablein=document.getElementById("<%=GridViewIn.ClientID %>");
   function RowInClick()
   {
           for (var i=0, j=tablein.tBodies[0].rows.length-1; i<j; i++) 
          {
            tablein.rows[i].onclick= function (i) 
            {
                var clicked = false;
                var dataRow = tablein.tBodies[0].rows[i];
                return function () 
                      {
                            if (clicked) 
                            {
                                dataRow.style.backgroundColor = "#ffffff";
                                clicked = false;
                            }
                            else 
                            {
                                dataRow.style.backgroundColor = "#BFDFFF";
                                clicked = true;
                            }
                        }
             }.call(this, i);
           }
   }
   RowInClick(); 
                        </script>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:content>

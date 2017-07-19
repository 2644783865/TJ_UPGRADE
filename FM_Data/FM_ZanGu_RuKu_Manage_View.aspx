<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="FM_ZanGu_RuKu_Manage_View.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_ZanGu_RuKu_Manage_View"
    Title="无标题页" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <link href="StyleFile/model.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
   function cancel() 
   {
     window.parent.document.getElementById('ButtonEditCancel').click();
   }
    </script>

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
                        入库单信息</div>
                    <div style="height: 400px;">
                        <yyc:SmartGridView ID="GridViewIn" runat="server" AutoGenerateColumns="False" ShowFooter="true" onrowdatabound="GridViewIn_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="WG_CODE" HeaderText="入库单号" />
                                <asp:BoundField DataField="WG_MARID" HeaderText="物料编码" />
                                <asp:BoundField DataField="MNAME" HeaderText="物料名称" />
                                <asp:BoundField DataField="GUIGE" HeaderText="规格" />
                                <asp:BoundField DataField="HMCODE" HeaderText="助记码" />
                                <asp:BoundField DataField="CGDW" HeaderText="单位" />
                                <asp:BoundField DataField="WG_RSNUM" HeaderText="数量" />
                                <asp:BoundField DataField="WG_UPRICE" HeaderText="单价" />
                                <asp:BoundField DataField="WG_AMOUNT" HeaderText="金额" />
                                <asp:BoundField DataField="WG_TAXRATE" HeaderText="税率" />
                                <asp:BoundField DataField="WG_CTAXUPRICE" HeaderText="含税单价" />
                                <asp:BoundField DataField="WG_CTAMTMNY" HeaderText="含税金额" />
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
           for (var i=1, j=tablein.tBodies[0].rows.length; i<j; i++) 
          {
            tablein.tBodies[0].rows[i].onclick= function (i) 
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
                                dataRow.style.backgroundColor = "#D1DDF1";
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
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_WorkAmount_Manage.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_WorkAmount_Manage1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        var isdisplay = true;
        function toggle(a, b,c) {

            var tb = document.getElementById("ctl00_PrimaryContent_GridView1");
                if (tb.rows[1].cells[a].style.display == "") {
                    for (var i = a; i <= b; i++)
                        for (var j = 1; j < tb.rows.length; j++) {
                        tb.rows[j].cells[i].style.display = "none";
                    }
                    tb.rows[0].cells[c].colSpan = 1;
                }
                else {
                    var tb = document.getElementById("ctl00_PrimaryContent_GridView1");
                    for (var i = a; i <= b; i++)
                        for (var j = 1; j < tb.rows.length; j++) {
                        tb.rows[j].cells[i].style.display = "";
                    }
                    tb.rows[0].cells[c].colSpan = (b - a + 2);
                }            
//            if (isdisplay) {
//                for(var i=a;i<=b;i++)
//                  for (var j = 1; j < tb.rows.length; j++) {
//                      tb.rows[j].cells[i].style.display = "none";
//                }
//                //tb.rows[2].childNodes[3].style.display="none";
//                //alert(tb.rows[0].innerHTML);
//                tb.rows[0].childNodes[c].colSpan = 1;
//                isdisplay = false;
//                 }
//            else {
//                var tb = document.getElementById("ctl00_PrimaryContent_GridView1");
//                for (var i = a; i <= b; i++)
//                   for (var j = 1; j < tb.rows.length; j++) {
//                    tb.rows[j].cells[i].style.display = "";
//                }
//                //tb.rows[2].childNodes[3].style.display="none";
//                //alert(tb.rows[0].innerHTML);
//                tb.rows[0].childNodes[c].colSpan = (b - a + 2);
//                isdisplay = true;
//            }
        }

</script>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="toptable grid" 
        AutoGenerateColumns="False" DataKeyNames="ENC_ID" 
        DataSourceID="SqlDataSource1" onrowcreated="GridView1_RowCreated">
        <Columns>
            <asp:CommandField ShowEditButton="True" ItemStyle-Width="30px" />
            <asp:BoundField DataField="ENC_ID" HeaderText="ENC_ID" ReadOnly="True" 
                SortExpression="ENC_ID" />
            <asp:BoundField DataField="ENC_TSNAME" HeaderText="ENC_TSNAME"  ReadOnly="True" 
                SortExpression="ENC_TSNAME" />
            <asp:BoundField DataField="ENC_PJID" HeaderText="ENC_PJID" 
                SortExpression="ENC_PJID" />
            <asp:BoundField DataField="ENC_PJNAME" HeaderText="ENC_PJNAME" 
                SortExpression="ENC_PJNAME" />
            <asp:BoundField DataField="ENC_ENGID" HeaderText="ENC_ENGID" 
                SortExpression="ENC_ENGID" />
            <asp:BoundField DataField="ENC_ENGNAME" HeaderText="ENC_ENGNAME" 
                SortExpression="ENC_ENGNAME" />
            <asp:BoundField DataField="ENC_DELIVERY" HeaderText="ENC_DELIVERY"  ReadOnly="True" 
                SortExpression="ENC_DELIVERY" />
            <asp:BoundField DataField="ENC_PREPARE" HeaderText="ENC_PREPARE"  ReadOnly="True" 
                SortExpression="ENC_PREPARE" />
            <asp:BoundField DataField="ENC_ENGTYPE" HeaderText="ENC_ENGTYPE"  ReadOnly="True" 
                SortExpression="ENC_ENGTYPE" />
            <asp:BoundField DataField="ENC_STFORCODE" HeaderText="ENC_STFORCODE"  ReadOnly="True" 
                SortExpression="ENC_STFORCODE" />
            <asp:BoundField DataField="ENC_DRAWCODE" HeaderText="ENC_DRAWCODE"  ReadOnly="True" 
                SortExpression="ENC_DRAWCODE" />
            <asp:BoundField DataField="ENC_DEVICECODE" HeaderText="ENC_DEVICECODE"  ReadOnly="True" 
                SortExpression="ENC_DEVICECODE" />
            <asp:BoundField DataField="ENC_DESIGNCOM" HeaderText="ENC_DESIGNCOM"  ReadOnly="True" 
                SortExpression="ENC_DESIGNCOM" />
            <asp:BoundField DataField="ENC_MODELCODE" HeaderText="ENC_MODELCODE"  ReadOnly="True" 
                SortExpression="ENC_MODELCODE" />
            <asp:BoundField DataField="ENC_RECVDATE" HeaderText="ENC_RECVDATE"  ReadOnly="True" 
                SortExpression="ENC_RECVDATE" />
            <asp:BoundField DataField="ENC_CONFNSHDATE" HeaderText="ENC_CONFNSHDATE"  ReadOnly="True" 
                SortExpression="ENC_CONFNSHDATE" />
            <asp:BoundField DataField="ENC_CONTYPE" HeaderText="ENC_CONTYPE"  ReadOnly="True" 
                SortExpression="ENC_CONTYPE" />
            <asp:BoundField DataField="ENC_DRAWSTATE" HeaderText="ENC_DRAWSTATE"  ReadOnly="True" 
                SortExpression="ENC_DRAWSTATE" />
            <asp:BoundField DataField="ENC_CLIENT" HeaderText="ENC_CLIENT"  ReadOnly="True" 
                SortExpression="ENC_CLIENT" />
            <asp:BoundField DataField="ENC_CONNOTE" HeaderText="ENC_CONNOTE"  ReadOnly="True" 
                SortExpression="ENC_CONNOTE" />
            <asp:BoundField DataField="ENC_TCCHGER" HeaderText="ENC_TCCHGER" 
                SortExpression="ENC_TCCHGER" />
            <asp:BoundField DataField="ENC_TCCHGERNM" HeaderText="ENC_TCCHGERNM" 
                SortExpression="ENC_TCCHGERNM" />
            <asp:BoundField DataField="ENC_TASKSTARTDATE" HeaderText="ENC_TASKSTARTDATE"  ReadOnly="True" 
                SortExpression="ENC_TASKSTARTDATE" />
            <asp:BoundField DataField="ENC_WORKDURATION" HeaderText="ENC_WORKDURATION"  ReadOnly="True" 
                SortExpression="ENC_WORKDURATION" />
            <asp:BoundField DataField="ENC_PLANFNSHDATE" HeaderText="ENC_PLANFNSHDATE"  ReadOnly="True" 
                SortExpression="ENC_PLANFNSHDATE" />
            <asp:BoundField DataField="ENC_STATEMENT" HeaderText="ENC_STATEMENT"  ReadOnly="True" 
                SortExpression="ENC_STATEMENT" />
            <asp:BoundField DataField="ENC_REMAINTIME" HeaderText="ENC_REMAINTIME"  ReadOnly="True" 
                SortExpression="ENC_REMAINTIME" />
            <asp:BoundField DataField="ENC_RLFNSHDURATION" HeaderText="ENC_RLFNSHDURATION"  ReadOnly="True" 
                SortExpression="ENC_RLFNSHDURATION" />
            <asp:BoundField DataField="ENC_PAINTINGPLAN" HeaderText="ENC_PAINTINGPLAN"  ReadOnly="True" 
                SortExpression="ENC_PAINTINGPLAN" />
            <asp:BoundField DataField="ENC_TECHSHARING" HeaderText="ENC_TECHSHARING"  ReadOnly="True" 
                SortExpression="ENC_TECHSHARING" />
            <asp:BoundField DataField="ENC_PLPRESCHEDULE" HeaderText="ENC_PLPRESCHEDULE"  ReadOnly="True" 
                SortExpression="ENC_PLPRESCHEDULE" />
            <asp:BoundField DataField="ENC_THIRDPART" HeaderText="ENC_THIRDPART"  ReadOnly="True" 
                SortExpression="ENC_THIRDPART" />
            <asp:BoundField DataField="ENC_SHIPTIME" HeaderText="ENC_SHIPTIME"  ReadOnly="True" 
                SortExpression="ENC_SHIPTIME" />
            <asp:BoundField DataField="ENC_ENGRNKSHPADJST" HeaderText="ENC_ENGRNKSHPADJST"  ReadOnly="True" 
                SortExpression="ENC_ENGRNKSHPADJST" />
            <asp:BoundField DataField="ENC_DESTINATION" HeaderText="ENC_DESTINATION"  ReadOnly="True" 
                SortExpression="ENC_DESTINATION" />
            <asp:BoundField DataField="ENC_COLPORTTIME" HeaderText="ENC_COLPORTTIME"  ReadOnly="True" 
                SortExpression="ENC_COLPORTTIME" />
            <asp:BoundField DataField="ENC_QUANTITY" HeaderText="ENC_QUANTITY"  ReadOnly="True" 
                SortExpression="ENC_QUANTITY" />
            <asp:BoundField DataField="ENC_ENGQNTY" HeaderText="ENC_ENGQNTY"  ReadOnly="True" 
                SortExpression="ENC_ENGQNTY" />
            <asp:BoundField DataField="ENC_ACCOUNTWGHT" HeaderText="ENC_ACCOUNTWGHT"  ReadOnly="True" 
                SortExpression="ENC_ACCOUNTWGHT" />
            <asp:BoundField DataField="ENC_PROVDMARWGHT" HeaderText="ENC_PROVDMARWGHT"  ReadOnly="True" 
                SortExpression="ENC_PROVDMARWGHT" />
            <asp:BoundField DataField="ENC_PERWGHT" HeaderText="ENC_PERWGHT"  ReadOnly="True" 
                SortExpression="ENC_PERWGHT" />
            <asp:BoundField DataField="ENC_PDLVRYPCNT" HeaderText="ENC_PDLVRYPCNT"  ReadOnly="True" 
                SortExpression="ENC_PDLVRYPCNT" />
            <asp:BoundField DataField="ENC_DEVICEWGHT" HeaderText="ENC_DEVICEWGHT"  ReadOnly="True" 
                SortExpression="ENC_DEVICEWGHT" />
            <asp:BoundField DataField="ENC_PDCTDLVRYPCNT" HeaderText="ENC_PDCTDLVRYPCNT"  ReadOnly="True" 
                SortExpression="ENC_PDCTDLVRYPCNT" />
            <asp:BoundField DataField="ENC_VOLUMN" HeaderText="ENC_VOLUMN"  ReadOnly="True" 
                SortExpression="ENC_VOLUMN" />
            <asp:BoundField DataField="ENC_NETWGHT" HeaderText="ENC_NETWGHT"  ReadOnly="True" 
                SortExpression="ENC_NETWGHT" />
            <asp:BoundField DataField="ENC_GROSSWGHT" HeaderText="ENC_GROSSWGHT"  ReadOnly="True" 
                SortExpression="ENC_GROSSWGHT" />
            <asp:BoundField DataField="ENC_VLMWGHTRATE" HeaderText="ENC_VLMWGHTRATE"  ReadOnly="True" 
                SortExpression="ENC_VLMWGHTRATE" />
            <asp:BoundField DataField="ENC_PACKMARATE" HeaderText="ENC_PACKMARATE"  ReadOnly="True" 
                SortExpression="ENC_PACKMARATE" />
            <asp:BoundField DataField="ENC_PACKAGEMAR" HeaderText="ENC_PACKAGEMAR"  ReadOnly="True" 
                SortExpression="ENC_PACKAGEMAR" />
            <asp:BoundField DataField="ENC_FINALVOLUME" HeaderText="ENC_FINALVOLUME"  ReadOnly="True" 
                SortExpression="ENC_FINALVOLUME" />
            <asp:BoundField DataField="ENC_PACKAGENUM" HeaderText="ENC_PACKAGENUM"  ReadOnly="True" 
                SortExpression="ENC_PACKAGENUM" />
            <asp:BoundField DataField="ENC_MANUFACTURE" HeaderText="ENC_MANUFACTURE"  ReadOnly="True" 
                SortExpression="ENC_MANUFACTURE" />
            <asp:BoundField DataField="ENC_SUPPLYMARSTYLE" HeaderText="ENC_SUPPLYMARSTYLE"  ReadOnly="True" 
                SortExpression="ENC_SUPPLYMARSTYLE" />
            <asp:BoundField DataField="ENC_STARTENGDATE" HeaderText="ENC_STARTENGDATE"  ReadOnly="True" 
                SortExpression="ENC_STARTENGDATE" />
            <asp:BoundField DataField="ENC_FNSHENGDATE" HeaderText="ENC_FNSHENGDATE"  ReadOnly="True" 
                SortExpression="ENC_FNSHENGDATE" />
            <asp:BoundField DataField="ENC_CONTRACTID" HeaderText="ENC_CONTRACTID"  ReadOnly="True" 
                SortExpression="ENC_CONTRACTID" />
            <asp:BoundField DataField="ENC_TASKCODE" HeaderText="ENC_TASKCODE"  ReadOnly="True" 
                SortExpression="ENC_TASKCODE" />
            <asp:BoundField DataField="ENC_PREPAYPCNT" HeaderText="ENC_PREPAYPCNT"  ReadOnly="True" 
                SortExpression="ENC_PREPAYPCNT" />
            <asp:BoundField DataField="ENC_SCHDLMNYPCNT" HeaderText="ENC_SCHDLMNYPCNT"  ReadOnly="True" 
                SortExpression="ENC_SCHDLMNYPCNT" />
            <asp:BoundField DataField="ENC_ACCOUNTPCNT" HeaderText="ENC_ACCOUNTPCNT"  ReadOnly="True" 
                SortExpression="ENC_ACCOUNTPCNT" />
            <asp:BoundField DataField="ENC_MNYNOTE" HeaderText="ENC_MNYNOTE"  ReadOnly="True" 
                SortExpression="ENC_MNYNOTE" />
            <asp:BoundField DataField="ENC_TOTALNOA" HeaderText="ENC_TOTALNOA"  ReadOnly="True" 
                SortExpression="ENC_TOTALNOA" />
            <asp:BoundField DataField="ENC_TOTALNOB" HeaderText="ENC_TOTALNOB"  ReadOnly="True" 
                SortExpression="ENC_TOTALNOB" />
            <asp:BoundField DataField="ENC_COSTSTC" HeaderText="ENC_COSTSTC"  ReadOnly="True" 
                SortExpression="ENC_COSTSTC" />
            <asp:BoundField DataField="ENC_CSTATEMENT" HeaderText="ENC_CSTATEMENT"  ReadOnly="True" 
                SortExpression="ENC_CSTATEMENT" />
            <asp:BoundField DataField="ENC_SENDDATE" HeaderText="ENC_SENDDATE"  ReadOnly="True" 
                SortExpression="ENC_SENDDATE" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ZCZJDMPConnectionString %>" 
        DeleteCommand="DELETE FROM [TBMP_ENGNUMCOLLECT] WHERE [ENC_ID] = @ENC_ID" 
        SelectCommand="SELECT * FROM [TBMP_ENGNUMCOLLECT]" 
        UpdateCommand="UPDATE [TBMP_ENGNUMCOLLECT] SET [ENC_PJID] = @ENC_PJID, [ENC_PJNAME] = @ENC_PJNAME, [ENC_ENGID] = @ENC_ENGID, [ENC_ENGNAME] = @ENC_ENGNAME, [ENC_TCCHGER] = @ENC_TCCHGER, [ENC_TCCHGERNM] = @ENC_TCCHGERNM WHERE [ENC_ID] = @ENC_ID">
        <DeleteParameters>
            <asp:Parameter Name="ENC_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ENC_TSNAME" Type="String" />
            <asp:Parameter Name="ENC_PJID" Type="String" />
            <asp:Parameter Name="ENC_PJNAME" Type="String" />
            <asp:Parameter Name="ENC_ENGID" Type="String" />
            <asp:Parameter Name="ENC_ENGNAME" Type="String" />
            <asp:Parameter Name="ENC_DELIVERY" Type="String" />
            <asp:Parameter Name="ENC_PREPARE" Type="String" />
            <asp:Parameter Name="ENC_ENGTYPE" Type="String" />
            <asp:Parameter Name="ENC_STFORCODE" Type="String" />
            <asp:Parameter Name="ENC_DRAWCODE" Type="String" />
            <asp:Parameter Name="ENC_DEVICECODE" Type="String" />
            <asp:Parameter Name="ENC_DESIGNCOM" Type="String" />
            <asp:Parameter Name="ENC_MODELCODE" Type="String" />
            <asp:Parameter Name="ENC_RECVDATE" Type="String" />
            <asp:Parameter Name="ENC_CONFNSHDATE" Type="String" />
            <asp:Parameter Name="ENC_CONTYPE" Type="String" />
            <asp:Parameter Name="ENC_DRAWSTATE" Type="String" />
            <asp:Parameter Name="ENC_CLIENT" Type="String" />
            <asp:Parameter Name="ENC_CONNOTE" Type="String" />
            <asp:Parameter Name="ENC_TCCHGER" Type="String" />
            <asp:Parameter Name="ENC_TCCHGERNM" Type="String" />
            <asp:Parameter Name="ENC_TASKSTARTDATE" Type="String" />
            <asp:Parameter Name="ENC_WORKDURATION" Type="String" />
            <asp:Parameter Name="ENC_PLANFNSHDATE" Type="String" />
            <asp:Parameter Name="ENC_STATEMENT" Type="String" />
            <asp:Parameter Name="ENC_REMAINTIME" Type="String" />
            <asp:Parameter Name="ENC_RLFNSHDURATION" Type="String" />
            <asp:Parameter Name="ENC_PAINTINGPLAN" Type="String" />
            <asp:Parameter Name="ENC_TECHSHARING" Type="String" />
            <asp:Parameter Name="ENC_PLPRESCHEDULE" Type="String" />
            <asp:Parameter Name="ENC_THIRDPART" Type="String" />
            <asp:Parameter Name="ENC_SHIPTIME" Type="String" />
            <asp:Parameter Name="ENC_ENGRNKSHPADJST" Type="String" />
            <asp:Parameter Name="ENC_DESTINATION" Type="String" />
            <asp:Parameter Name="ENC_COLPORTTIME" Type="String" />
            <asp:Parameter Name="ENC_QUANTITY" Type="Double" />
            <asp:Parameter Name="ENC_ENGQNTY" Type="Double" />
            <asp:Parameter Name="ENC_ACCOUNTWGHT" Type="Double" />
            <asp:Parameter Name="ENC_PROVDMARWGHT" Type="Double" />
            <asp:Parameter Name="ENC_PERWGHT" Type="Double" />
            <asp:Parameter Name="ENC_PDLVRYPCNT" Type="Double" />
            <asp:Parameter Name="ENC_DEVICEWGHT" Type="Double" />
            <asp:Parameter Name="ENC_PDCTDLVRYPCNT" Type="Double" />
            <asp:Parameter Name="ENC_VOLUMN" Type="Double" />
            <asp:Parameter Name="ENC_NETWGHT" Type="Double" />
            <asp:Parameter Name="ENC_GROSSWGHT" Type="Double" />
            <asp:Parameter Name="ENC_VLMWGHTRATE" Type="Double" />
            <asp:Parameter Name="ENC_PACKMARATE" Type="Double" />
            <asp:Parameter Name="ENC_PACKAGEMAR" Type="String" />
            <asp:Parameter Name="ENC_FINALVOLUME" Type="Double" />
            <asp:Parameter Name="ENC_PACKAGENUM" Type="Double" />
            <asp:Parameter Name="ENC_MANUFACTURE" Type="String" />
            <asp:Parameter Name="ENC_SUPPLYMARSTYLE" Type="String" />
            <asp:Parameter Name="ENC_STARTENGDATE" Type="String" />
            <asp:Parameter Name="ENC_FNSHENGDATE" Type="String" />
            <asp:Parameter Name="ENC_CONTRACTID" Type="String" />
            <asp:Parameter Name="ENC_TASKCODE" Type="String" />
            <asp:Parameter Name="ENC_PREPAYPCNT" Type="Double" />
            <asp:Parameter Name="ENC_SCHDLMNYPCNT" Type="Double" />
            <asp:Parameter Name="ENC_ACCOUNTPCNT" Type="Double" />
            <asp:Parameter Name="ENC_MNYNOTE" Type="String" />
            <asp:Parameter Name="ENC_TOTALNOA" Type="String" />
            <asp:Parameter Name="ENC_TOTALNOB" Type="String" />
            <asp:Parameter Name="ENC_COSTSTC" Type="String" />
            <asp:Parameter Name="ENC_CSTATEMENT" Type="String" />
            <asp:Parameter Name="ENC_SENDDATE" Type="String" />
            <asp:Parameter Name="ENC_ID" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

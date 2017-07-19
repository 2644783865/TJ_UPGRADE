<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCTxtDate.ascx.cs" Inherits="ZCZJ_DPF.Controls.UCTxtDate" %>
<script language="javascript" type="text/javascript">
function ShowCalendar()
{
    document.all.layerin.style.display='block';  
    document.all.layerout.style.display='block'; 
    }
    
     function DisCalendar(controlID)//!window.document.getElementById(controlID).visible()
    {
    if(document.all.layerin.style.display=='block'
        &&!(window.document.getElementById(controlID).focus())){
     document.all.layerin.style.display='none';  
     document.all.layerout.style.display='block';  
     }
     else{
     document.all.layerin.style.display='block';  
     document.all.layerout.style.display='block';
     //window.document.getElementById(controlID).focus();
     }
}
</script>
<asp:UpdatePanel ID="upDate" runat="server">
<ContentTemplate> 
<div id="layerout" style="z-index: 0;  display:block ;">
<asp:TextBox ID="txtDate" ReadOnly="true" Text="" runat="server"></asp:TextBox>
    <div id="layerin" align="center" style="position: absolute; z-index: 3;    background-color: #fff; display:none">
    <asp:Calendar ID="calDate" runat="server" OnVisibleMonthChanged="calDate_VisibleMonthChanged"
        onselectionchanged="calDate_SelectionChanged"></asp:Calendar>
    </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>

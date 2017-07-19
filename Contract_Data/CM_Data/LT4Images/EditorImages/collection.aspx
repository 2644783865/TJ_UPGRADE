<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Inherits="LongTrueComEditor.PageCollection"%>
<%@ Import Namespace="LongTrueComEditor" %>
<html>
<head runat="server">
<title><%=ResourceManager.GetString("getpage")%></title>
<meta http-equiv="pragma" content="no-cache" />
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<base target="_self" />
<link href="stylesheet.css" rel="stylesheet" type="text/css" />
<script language=javascript>
Request = {
 QueryString : function(item){
  var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)","i"));
  return svalue ? svalue[1] : svalue;
 }
}

function loading()
{
document.getElementById("loading").style.visibility="visible";
return true;
}

function addeditor()
{
if(confirm('<%=ResourceManager.GetString("getpageconfirm")%>'))
{
parent.inserObject(parent.document.getElementById(Request.QueryString("name")).contentWindow,'getpage',document.getElementById("tempcontent").value);
parent.popupmenu.hide();
parent.rcmenu.hide();
}
}
</script>
</head>
<body bgcolor="#f9f8f7" leftmargin=5 topmargin=5>
<form runat=server>
<div id="loading" style="border-right: #333333 1px dashed; border-top: #333333 1px dashed;
font-size: 9pt; visibility: hidden; border-left: #333333 1px dashed;
width: 200px; color: #000000; border-bottom: #333333 1px dashed; position: absolute; height: 60px; background-color: #ffffff">
<center>
<br />
<%=ResourceManager.GetString("getpageloading")%></center>
<br />
<center>
<asp:Button ID="canceloading" runat="server" Style="border-top-style: dashed; border-right-style: dashed;
border-left-style: dashed; border-bottom-style: dashed" />&nbsp;</center>
<br />
</div>
<fieldset><legend><%=ResourceManager.GetString("getpage")%></legend>
<table align=center>
<tr>
<td style="height: 46px; width: 360px;" >
<asp:TextBox ID="txtUrl" runat="server" Text="http://" Width="305px"></asp:TextBox>
<br />
<%=ResourceManager.GetString("getpagetype")%>：<asp:DropDownList ID="seltype" runat="server" />
<asp:Button ID="btnReturn" runat="server" OnClientClick="loading()" OnClick="btnReturn_Click" />
<input type=button name="close" onclick="parent.popupmenu.hide();parent.rcmenu.hide();" value='<%=ResourceManager.GetString("close")%>'></td>
</tr>
</table></fieldset>
<asp:HiddenField ID="tempcontent" runat="server" />
</form>
</body>
<script type="text/javascript">
var load=document.getElementById('loading');
resizeLoad();
window.setInterval("resizeLoad()",10);
function resizeLoad()
{
load.style.top = parseInt((document.body.clientHeight-load.offsetHeight)/2+document.body.scrollTop);
load.style.left = parseInt((document.body.clientWidth-load.offsetWidth)/2+document.body.scrollLeft);
}
</script>
</html>

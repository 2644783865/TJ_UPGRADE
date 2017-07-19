<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Inherits="Word_dntb.Importword" %>
<%@ Import Namespace="LongTrueComEditor" %>
<html>
<head>
<title><%=ResourceManager.GetString("importword")%></title>
<meta http-equiv="pragma" content="no-cache" />
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<base target="_self" />
<style type="text/css">
body,a,table,input,button{font-size:9pt;font-family:Verdana,Arial}
</style>
<script type="text/javascript" language="javascript">
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
if(confirm('<%=ResourceManager.GetString("importwordconfirm")%>'))
{
    parent.plugin_execommand(parent.document.getElementById(Request.QueryString("name")).contentWindow,document.getElementById("worddoc").value);
　　parent.popupmenu.hide();
　　parent.rcmenu.hide();
}
}
</script>
</head>
<body bgcolor="#f9f8f7" leftmargin=5 topmargin=5>
    <form id="form1" runat="server">
    <fieldset><legend><%=ResourceManager.GetString("importword")%></legend>
        <br /><div align=center>
        <asp:FileUpload ID="FileUpload1" runat="server" Height="20px" Width="250px" />
        <asp:Button ID="btnUpload" runat="server" OnClientClick="loading()" OnClick="btnUpload_Click" /><br />
        <asp:CheckBox ID="saveword" runat="server" Width="206px" /><br />
        <asp:HiddenField ID="worddoc" runat="server" /></div></fieldset>
        
                    <div id="loading" style="border-right: #333333 1px dashed; border-top: #333333 1px dashed;
                        font-size: 9pt; visibility: hidden; border-left: #333333 1px dashed;
                        width: 270px; color: #000000; border-bottom: #333333 1px dashed; position: absolute; height: 80px; background-color: #ffffff">
                        <center>
                            <br />
                            <%=ResourceManager.GetString("loading")%></center>
                        <br />
                        <center>
                            <asp:Button ID="canceloading" runat="server" Style="border-top-style: dashed; border-right-style: dashed;
                                border-left-style: dashed; border-bottom-style: dashed" />&nbsp;</center>
                        <br />
                    </div>
    </form>
</body>
<script language="javascript">
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
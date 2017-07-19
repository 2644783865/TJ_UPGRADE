<%@ Page language="c#" AutoEventWireup="true"  Inherits="LongTrueComEditor.PageCode" %>
<%@ Import Namespace="LongTrueComEditor" %>
<html>
<head>
<meta http-equiv="pragma" content="no-cache" />
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<base target="_self" />
<style type="text/css">
body,a,table,input,select{font-size:12px;font-family:宋体,Verdana,Arial}
</style>
<script type="text/javascript">
var userAgent = navigator.userAgent.toLowerCase();
var is_ie = (userAgent.indexOf('msie') != -1);
var type;

Request = {
 QueryString : function(item){
  var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)","i"));
  return svalue ? svalue[1] : svalue;
 }
}

if (window.dialogArguments)
{
type = dialogArguments;
}
else
{
type=Request.QueryString("type");
}
function insertCustom()
{
    var arr=new Array;
    if(document.getElementById("number").value!="")
    {
    if(is_ie)
    {
    window.returnValue=document.getElementById("number").value;
    }
　　else
　　{
　　window.opener.inserObject(null,type,document.getElementById("number").value);
　　}
　　window.close();
　  }
}
</script>
</head>
<body leftmargin="5" topmargin="0">
<form runat=server>
<br />
<fieldset>
<span id="prompt"></span><br/><input id="number" style="width: 170px" type="text" />&nbsp;<br />
    <input onclick="insertCustom()" type="button" value='<%=ResourceManager.GetString("insert")%>'/>
    <input onclick="window.close();" type="button" value='<%=ResourceManager.GetString("close")%>'/><br />
</fieldset></form>
</body>
<script language=javascript>
if(is_ie)
{
    document.body.bgColor="ButtonFace";
}
else
{
    document.body.bgColor="#E0E0E0";
}

if(type=="emot")
{
   document.getElementById("prompt").innerHTML='<%=ResourceManager.GetString("emotpath")%>';
   document.getElementById("number").value="http://";
   document.title='<%=ResourceManager.GetString("emotpath")%>';
}
else if(type=="marquee")
{
   document.getElementById("prompt").innerHTML='<%=ResourceManager.GetString("flyspeed")%>';
   document.getElementById("number").value="3";
   document.title='<%=ResourceManager.GetString("flyspeed")%>';
}
else if(type=="font")
{
   document.getElementById("prompt").innerHTML='<%=ResourceManager.GetString("setupfont")%>';
   document.getElementById("number").value="Arial";
   document.title='<%=ResourceManager.GetString("setupfont")%>';
}
</script></html>
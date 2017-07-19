<%@ Page language="c#" AutoEventWireup="true"  Inherits="LongTrueComEditor.PageCode"%>
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
Request = {
QueryString : function(item){
var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)","i"));
return svalue ? svalue[1] : svalue;
}
}
var type=Request.QueryString("type");

function insertOnlineCode()
{
var arr=new Array;
if(document.getElementById("number").value!="")
{
if(type.indexOf("_popup_qq_menu")!=-1)
{
arr[0]=document.getElementById("number").value;
arr[1]=document.getElementById("qqstyle").value;
parent.inserObject(parent.document.getElementById(Request.QueryString("name")).contentWindow,'qq',arr);
}
else if(type.indexOf("_popup_msn_menu")!=-1)
{
parent.inserObject(parent.document.getElementById(Request.QueryString("name")).contentWindow,'msn',document.getElementById("number").value);
}
else
{
parent.inserObject(parent.document.getElementById(Request.QueryString("name")).contentWindow,'icq',document.getElementById("number").value);
}
}
parent.popupmenu.hide();
parent.rcmenu.hide();

}
</script>
</head>
<body bgcolor="#f9f8f7" leftmargin="5" topmargin="0">
<form runat="server">
<br />
<fieldset>
<span id="prompt"></span><br/><input id="number" onfocus="this.value='';" style="width: 170px" type="text" />&nbsp;<br />
<label id="showstyle" style="visibility:hidden"><%=ResourceManager.GetString("qqstyle")%><select id="qqstyle">
<option selected="selected"  value="1">1</option>
<option value="2">2</option>
<option value="3">3</option>
<option value="4">4</option>
<option value="5">5</option>
<option value="6">6</option>
<option value="7">7</option>
<option value="8">8</option>
<option value="9">9</option>
<option value="10">10</option>
<option value="11">11</option>
<option value="12">12</option>
<option value="13">13</option>
</select></label>
<input onclick="insertOnlineCode()" type="button" value='<%=ResourceManager.GetString("insert")%>'/>
<input onclick="parent.popupmenu.hide();parent.rcmenu.hide();" type="button" value='<%=ResourceManager.GetString("close")%>'/><br />
<span style="visibility:hidden" id="viewstyle"><a href="http://is.qq.com/webpresence/code.shtml" target=_blank><%=ResourceManager.GetString("viewstyle")%></a></span>
</fieldset></form>
</body>
<script type="text/javascript" language="javascript">
if(type.indexOf("_popup_qq_menu")!=-1)
{
document.getElementById("prompt").innerHTML='<%=ResourceManager.GetString("inputqq")%>';
document.getElementById("showstyle").style.visibility="visible";
document.getElementById("viewstyle").style.visibility="visible";
document.title='<%=ResourceManager.GetString("qq")%>';
}
else if(type.indexOf("_popup_msn_menu")!=-1)
{
document.getElementById("prompt").innerHTML='<%=ResourceManager.GetString("inputmsn")%>';
document.title='<%=ResourceManager.GetString("msn")%>';
}
else
{
document.getElementById("prompt").innerHTML='<%=ResourceManager.GetString("inputicq")%>';
document.title='<%=ResourceManager.GetString("icq")%>';
}
</script></html>

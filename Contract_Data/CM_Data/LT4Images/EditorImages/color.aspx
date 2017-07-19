<%@ Page language="c#" AutoEventWireup="true" %>
<%@ Import Namespace="LongTrueComEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title><%=ResourceManager.GetString("customcolor")%></title>
<meta http-equiv="pragma" content="no-cache" />
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<base target="_self" />
<link href="stylesheet.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript">
var userAgent = navigator.userAgent.toLowerCase();
var is_ie = (userAgent.indexOf('msie') != -1);
var SelRGB = '#000000';
var DrRGB = '';
var SelGRAY = '120';
var type;
Request = {
QueryString : function(item){
var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)","i"));
return svalue ? svalue[1] : svalue;
}
}
if (!is_ie)
{
type=Request.QueryString("type");
}
var hexch = new Array('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F');
// 声明一个全局对象Namespace，用来注册命名空间
Namespace = new Object();
// 全局对象仅仅存在register函数，参数为名称空间全路径，如"Grandsoft.GEA"
Namespace.register = function(fullNS)
{
// 将命名空间切成N部分, 比如Grandsoft、GEA等
var nsArray = fullNS.split('.');
var sEval = "";
var sNS = "";   for (var i = 0; i < nsArray.length; i++)
{
if (i != 0) sNS += ".";
sNS += nsArray[i];
// 依次创建构造命名空间对象（假如不存在的话）的语句
// 比如先创建Grandsoft，然后创建Grandsoft.GEA，依次下去
sEval += "if (typeof(" + sNS + ") == 'undefined') " + sNS + " = new Object();"
}
if (sEval != "") eval(sEval);
}
// 注册命名空间Grandsoft.GEA, Grandsoft.GCM
Namespace.register("SelectColor");
SelectColor.Obj = function(){}
SelectColor.Obj.prototype = {
getRGB  :function (){    return document.getElementById("RGB");},
getGRAY :function (){    return document.getElementById("GRAY");},
getGrayTable    :function (){    return document.getElementById("GrayTable");},
getSelColor     :function (){    return document.getElementById("SelColor");},
getShowColor    :function (){    return document.getElementById("ShowColor");}
}
var colorObj = new SelectColor.Obj;
//设置颜色
SelectColor.setColor = function (color)
{
colorObj.getGRAY().innerHTML = "120";
colorObj.getRGB().innerHTML = color;
SelRGB = color;
this.EndColor();
}
SelectColor.ToHex = function (n)
{
var h, l;
n = Math.round(n);
l = n % 16;
h = Math.floor((n / 16)) % 16;
return (hexch[h] + hexch[l]);
}
SelectColor.DoColor = function (c, l)
{
var r, g, b;
r = '0x' + c.substring(1, 3);
g = '0x' + c.substring(3, 5);
b = '0x' + c.substring(5, 7);
if(l > 120)
{
l = l - 120;
r = (r * (120 - l) + 255 * l) / 120;
g = (g * (120 - l) + 255 * l) / 120;
b = (b * (120 - l) + 255 * l) / 120;
}
else
{
r = (r * l) / 120;
g = (g * l) / 120;
b = (b * l) / 120;
}
return '#' + this.ToHex(r) + this.ToHex(g) + this.ToHex(b);
}
SelectColor.EndColor = function ()
{
var i;
if(DrRGB != SelRGB)
{
DrRGB = SelRGB;
for(i = 0; i <= 30; i ++)
{
colorObj.getGrayTable().rows[i].style.backgroundColor = this.DoColor(SelRGB, 240 - i * 8);
}
}
colorObj.getSelColor().value = this.DoColor(colorObj.getRGB().innerHTML, colorObj.getGRAY().innerHTML );
colorObj.getShowColor().style.backgroundColor = colorObj.getSelColor().value;
}
SelectColor.GetFirefoxColorValue = function (c)
{
var r, g, b;
var ch = c.substring(4, c.length -1);
var chs = new Array();
chs = ch.split(",");
r = chs [0];//'0x' + ch.substring(1, 3);
g = chs [1];//'0x' + ch.substring(3, 5);
b = chs [2];//'0x' + ch.substring(5, 7);
return '#' + this.ToHex(r) + this.ToHex(g) + this.ToHex(b);
}
//事件
SelectColor.clickColorTable = function (event)
{
if(event.srcElement!=null )
{
eventObj = event.srcElement;
SelRGB = eventObj.style.backgroundColor.toUpperCase();
}
else
{
eventObj = event.target
SelRGB = this.GetFirefoxColorValue(eventObj.style.backgroundColor.toUpperCase());
}
this.EndColor();
}
SelectColor.mouseoverColorTable = function (event)
{
if(event.srcElement!=null )
{
eventObj = event.srcElement;
colorObj.getRGB().innerHTML = eventObj.style.backgroundColor.toUpperCase();
}
else
{
eventObj = event.target
colorObj.getRGB().innerHTML = this.GetFirefoxColorValue(eventObj.style.backgroundColor.toUpperCase());
}
this.EndColor();
}
SelectColor.mouseoutColorTable = function (event)
{
colorObj.getRGB().innerHTML = SelRGB;
this.EndColor();
}
SelectColor.clickGrayTable = function (event)
{
var eventObj;
if(event.srcElement!=null )
{
eventObj = event.srcElement;
}
else
{
eventObj = event.target
}
SelGRAY = eventObj.title;
this.EndColor();
}
SelectColor.mouseoverGrayTable = function (event)
{
var eventObj;
if(event.srcElement!=null )
{
eventObj = event.srcElement;
}
else
{
eventObj = event.target
}
colorObj.getGRAY().innerHTML = eventObj.title;
this.EndColor();
}
SelectColor.mouseoutGrayTable = function (event)
{
colorObj.getGRAY().innerHTML = SelGRAY;
this.EndColor();
}
function insertColor()
{
if (is_ie)
{
　　window.returnValue = document.getElementById("SelColor").value;
}
else
{
if(type!="forecolor")
{
window.opener.inserObject(null,type,document.getElementById("SelColor").value);
}
else
{
window.opener.format(null,type,document.getElementById("SelColor").value);
}
}
　　window.close();
}
window.focus();
</script>
</head>
<body topmargin="2">
<center>
<table cellspacing="10"  border="1"  width="265" cellpadding="0">
<tr>
<td>
<table id="ColorTable" style="border: 0; cursor: pointer;" cellspacing="0" cellpadding="0"
onclick="SelectColor.clickColorTable(event)" onmouseover="SelectColor.mouseoverColorTable(event)"
onmouseout="SelectColor.mouseoutColorTable(event)">
<script language="JavaScript" type="text/javascript">
function wc(r, g, b, n)
{
r = ((r * 16 + r) * 3 * (15 - n) + 0x80 * n) / 15;
g = ((g * 16 + g) * 3 * (15 - n) + 0x80 * n) / 15;
b = ((b * 16 + b) * 3 * (15 - n) + 0x80 * n) / 15;
document.write('<td style="background-color:#' + SelectColor.ToHex(r) + SelectColor.ToHex(g) + SelectColor.ToHex(b) + '; height:8px; width:8px;"></td>');
}
var cnum = new Array(1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0);
for(i = 0; i < 16; i ++)
{
document.write('<tr>');
for(j = 0; j < 30; j ++)
{
n1 = j % 5;
n2 = Math.floor(j / 5) * 3;
n3 = n2 + 3;
wc((cnum[n3] * n1 + cnum[n2] * (5 - n1)),
(cnum[n3 + 1] * n1 + cnum[n2 + 1] * (5 - n1)),
(cnum[n3 + 2] * n1 + cnum[n2 + 2] * (5 - n1)), i);
}
document.writeln('</tr>');
}
</script>
</table>
</td>
<td>
<table id="GrayTable" style="border: 0; cursor: pointer;" cellspacing="0" cellpadding="0"
onclick="SelectColor.clickGrayTable(event)" onmouseover="SelectColor.mouseoverGrayTable(event)"
onmouseout="SelectColor.mouseoutGrayTable(event)">
<script language="JavaScript" type="text/javascript">
for(i = 255; i >= 0; i -= 8.5)
{
document.write('<tr style="background-color:#' + SelectColor.ToHex(i) + SelectColor.ToHex(i) + SelectColor.ToHex(i) + ';"><td title="' + Math.floor(i * 16 / 17) + '" style="height:4px;width:20px;" ></td></tr>');
}
</script>
</table>
</td>
</tr>
</table>
</center>
<table align="center" border="1" cellpadding="0" cellspacing="10" width="265">
<tr>
<td align="center" rowspan="2">
<table id="ShowColor" border="0" cellpadding="0" cellspacing="0" height="30" width="80">
<tr>
<td>
</td>
</tr>
</table>
<input class="text" size="7" id="SelColor" onblur="SelectColor.setColor(value)" value="#000000"  type="text" />
</td>
<td rowspan="2" style="line-height: 16pt" width="85">
<%=ResourceManager.GetString("basecolor")%>:<span id="RGB">#000000</span><br />
<%=ResourceManager.GetString("brightness")%>:<span id="GRAY">120</span>
</td>
<td align="center" style="width: 55px; height: 33px">
<input class="button" type="button" onclick="insertColor()" value="<%=ResourceManager.GetString("ok")%>" />
</td>
</tr>
<tr>
<td align="center" style="width: 55px; height: 31px">
<input class="button" onclick="window.close();" type="button" value="<%=ResourceManager.GetString("close2")%>" />
</td>
</tr>
</table>
<script language=javascript>
if(is_ie)
{
document.body.bgColor="ButtonFace";
}
else
{
document.body.bgColor="#E0E0E0";
}
</script>
</body>
</html>

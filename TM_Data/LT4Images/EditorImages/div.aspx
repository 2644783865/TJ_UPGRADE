<%@ Page language="c#" AutoEventWireup="true" Inherits="LongTrueComEditor.PageCode"%>
<%@ Import Namespace="LongTrueComEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Pragma" content="no-cache" />
<base target="_self" />
<meta http-equiv="Content-Language" content="zh-cn" />
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link href="stylesheet3.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript">
var userAgent = navigator.userAgent.toLowerCase();
var is_ie = (userAgent.indexOf('msie') != -1);
var popMenu = new dhLayer();
var ecolorPopup=null;
function colordialogmouseout(obj){
obj.style.borderColor="";
obj.bgColor="";}
function colordialogmouseover(obj){
obj.style.borderColor="#0A66EE";
obj.bgColor="#EEEEEE";}
function colordialogmousedown(color,type)
{
$("d_"+type).value=color;
$("s_"+type).style.backgroundColor=color;
popMenu.hide();
}
function colordialogmore(type)
{
alert('<%=ResourceManager.GetString("colorcode")%>');
$("d_"+type).value='';
$("d_"+type).focus();
popMenu.hide();
}
function colordialog(type){
var e=event.srcElement? event.srcElement : event.target;
e.onkeyup=colordialog;
ecolorPopup=e;
var ocbody;
var colorlist=new Array(40);
popMenu.border = "solid #999999 1px";
colorlist[0]="#000000";	colorlist[1]="#993300";	colorlist[2]="#333300";	colorlist[3]="#003300";
colorlist[4]="#003366";	colorlist[5]="#000080";	colorlist[6]="#333399";	colorlist[7]="#333333";
colorlist[8]="#800000";	colorlist[9]="#FF6600";	colorlist[10]="#808000";colorlist[11]="#008000";
colorlist[12]="#008080";colorlist[13]="#0000FF";colorlist[14]="#666699";colorlist[15]="#808080";
colorlist[16]="#FF0000";colorlist[17]="#FF9900";colorlist[18]="#99CC00";colorlist[19]="#339966";
colorlist[20]="#33CCCC";colorlist[21]="#3366FF";colorlist[22]="#800080";colorlist[23]="#999999";
colorlist[24]="#FF00FF";colorlist[25]="#FFCC00";colorlist[26]="#FFFF00";colorlist[27]="#00FF00";
colorlist[28]="#00FFFF";colorlist[29]="#00CCFF";colorlist[30]="#993366";colorlist[31]="#CCCCCC";
colorlist[32]="#FF99CC";colorlist[33]="#FFCC99";colorlist[34]="#FFFF99";colorlist[35]="#CCFFCC";
colorlist[36]="#CCFFFF";colorlist[37]="#99CCFF";colorlist[38]="#CC99FF";colorlist[39]="#FFFFFF";
ocbody = "";
ocbody += "<table CELLPADDING=0 CELLSPACING=3>";
ocbody += "<tr height='20' width='20'><td align='center'><table style='border:1px solid #808080;' width='12' height='12' bgcolor='"+$("d_"+type).value+"'><tr><td></td></tr></table></td><td bgcolor='eeeeee' colspan='7' style='font-size:12px;' align='center'><%=ResourceManager.GetString("nowcolor")%></td></tr>";
for(var i=0;i<colorlist.length;i++){
if(i%8==0)
ocbody += "<tr>";
ocbody += "<td width='14' height='16' style='border:1px solid;' onMouseOut='parent.colordialogmouseout(this);' onMouseOver='parent.colordialogmouseover(this);' onMouseDown='parent.colordialogmousedown(\""+colorlist[i]+"\",\""+type+"\")' align='center' valign='middle'><table style='border:1px solid #808080;' width='12' height='12' bgcolor='"+colorlist[i]+"'><tr><td></td></tr></table></td>";
if(i%8==7)
ocbody += "</tr>";}
ocbody += "<tr><td align='center' height='22' colspan='8' onMouseOut='parent.colordialogmouseout(this);' onMouseOver='parent.colordialogmouseover(this);' style='border:1px solid;font-size:12px;cursor:default;' onMouseDown='parent.colordialogmore(\""+type+"\")'><%=ResourceManager.GetString("customcolor")%></td></tr>";
ocbody += "</table>";
popMenu.content = ocbody;
popMenu.show(158,147,document.body);}
function dhLayer(){
var dh = this;
this.content = null;
this.background = "#f9f8f7";
this.border = null;
this.fontSize = "12px";
this.padding = "0px";
this.cursor = "pointer";
if(is_ie){
var layer = window.createPopup();
}else{
var layer = document.createElement("DIV");}
this.show = function(w,h,o){
if(is_ie){
var l = document.body.scrollLeft+event.clientX;
var t = document.body.scrollTop+event.clientY;
layer.document.body.innerHTML = this.content;
layer.document.body.oncontextmenu = function(){return false};
layer.document.body.style.background = this.background;
layer.document.body.style.border = this.border;
layer.document.body.style.fontSize = this.fontSize;
layer.document.body.style.padding = this.padding;
layer.document.body.style.cursor = this.cursor;
layer.show(l,t,w,h,o);
}else{
w = w+"px";
h = h+"px";
var l = window.event.clientX+"px";
var t = window.event.clientY+"px";
layer.id = "dhLayer";
layer.innerHTML = this.content;
layer.style.background = this.background;
layer.style.border = this.border;
layer.style.fontSize = this.fontSize;
layer.style.zIndex = "99";
layer.style.width = w;
layer.style.height = h;
layer.style.position = "absolute";
layer.style.left = l;
layer.style.top = t;
layer.style.padding = this.padding;
layer.style.cursor = this.cursor;
layer.style.display = "block";
if(document.getElementById('dhLayer')!=null){
o.replaceChild(layer,document.getElementById('dhLayer'));
}else{
o.appendChild(layer);}}}
this.hide = function(){
if(is_ie){
layer.hide();
}else{
layer.style.display = "none";}}}
(function () {
if (! window.Window) return;
Window.constructor.prototype.__defineGetter__("event", function(){
var o = arguments.callee.caller;
var e;
while(o != null){
e = o.arguments[0];
if(e && (e.constructor == Event || e.constructor == MouseEvent)) return e;
o = o.caller;}
return null;});})();
function $()
{
var elements = new Array();
for (var i = 0; i < arguments.length; i++)
{
var element = arguments[i];
if (typeof element == 'string')
element = document.getElementById(element);
if (arguments.length == 1)
return element;
elements.push(element);
}
return elements;
}
var divs = new Array;
var sAction;
var sTitle;
var oSeletion;
var sRangeType;
var sAlign = "";
var sBorder = "1";
var sWidth = "";
var sHeight= "";
var sBorderColor = "#cccccc";
var sBgColor = "#ffffff";
var sWidthUnit = "px";
var sHeightUnit = "px";
var bCheck = true;
var bCheck2 = true;
var bWidthDisable = false;
var bHeightDisable =false;
var sWidthValue = "300";
var sHeightValue= "100";
var sLeft="0";
var sTop="0";
var sZindex ="50";
var sBorderStyle="solid";
var sWordBreak ="normal";
var sOverFlow="visible";
var sPosition="relative";
if(is_ie)
{
if (dialogArguments!=null)
{
divs = dialogArguments;
sAction = "MOD";
sTitle = '<%=ResourceManager.GetString("mofdiv")%>';
sWidth =divs[0];
sHeight=divs[1]
sAlign=divs[2];
sBorder=divs[3]?parseInt(divs[3]):"1";
sBgColor=divs[4];
sBorderColor=divs[5];
sWordBreak=divs[6];
sPosition=divs[7];
sLeft=divs[8]?parseInt(divs[8]):"0";
sTop=divs[9]?parseInt(divs[9]):"0";
sZindex=divs[10];
sBorderStyle=divs[11];
sOverFlow=divs[12];
}
else
{
sAction = "INSERT";
sTitle = '<%=ResourceManager.GetString("insertdiv")%>';
}
}
else
{
divs=window.opener.GetDiv();
if(divs[0]!=null)
{
sAction = "MOD";
sTitle = '<%=ResourceManager.GetString("mofdiv")%>';
sWidth =divs[0];
sHeight=divs[1]
sAlign=divs[2];
sBorder=divs[3]?parseInt(divs[3]):"1";
sBgColor=divs[4];
if(divs[5]==null)
{
sBorderColor="#cccccc";
}
else
{
sBorderColor=divs[5];
}
sWordBreak=divs[6];
sPosition=divs[7];
sLeft=divs[8]?parseInt(divs[8]):"0";
sTop=divs[9]?parseInt(divs[9]):"0";
sZindex=divs[10];
sBorderStyle=divs[11];
sOverFlow=divs[12];
}
else
{
sAction = "INSERT";
sTitle = '<%=ResourceManager.GetString("insertdiv")%>';
}
}
document.write("<TITLE>" + sTitle + "</TITLE>");
function InitDocument(){
SearchSelectValue($("d_align"), sAlign.toLowerCase());
if (sAction == "MOD"){
if (sWidth == ""){
bCheck = false;
bWidthDisable = true;
sWidthValue = "100";
sWidthUnit = "%";
}else{
bCheck = true;
bWidthDisable = false;
if (sWidth.substr(sWidth.length-1) == "%"){
sWidthValue = sWidth.substring(0, sWidth.length-1);
sWidthUnit = "%";
}else{
sWidthUnit = "";
sWidthValue = parseInt(sWidth);
if (isNaN(sWidthValue)) sWidthValue = "";
}
}
if (sHeight == ""){
bCheck2 = false;
bHeightDisable = true;
sHeightValue = "100";
sHeightUnit = "%";
}else{
bCheck2 = true;
bHeightDisable = false;
if (sHeight.substr(sHeight.length-1) == "%"){
sHeightValue = sHeight.substring(0, sHeight.length-1);
sHeightUnit = "%";
}else{
sHeightUnit = "";
sHeightValue = parseInt(sHeight);
if (isNaN(sHeightValue)) sHeightValue = "";
}
}
}
switch(sWidthUnit){
case "%":
$("d_widthunit").selectedIndex = 1;
break;
default:
sWidthUnit = "";
$("d_widthunit").selectedIndex = 0;
break;
}
switch(sHeightUnit){
case "%":
$("d_heightunit").selectedIndex = 1;
break;
default:
sWidthUnit = "";
$("d_heightunit").selectedIndex = 0;
break;
}
$("d_border").value = sBorder;
$("d_widthvalue").value = sWidthValue;
$("d_heightvalue").value = sHeightValue;
$("d_widthvalue").disabled = bWidthDisable;
$("d_heightvalue").disabled = bHeightDisable;
$("d_widthunit").disabled = bWidthDisable;
$("d_heightunit").disabled = bHeightDisable;
$("d_bordercolor").value = sBorderColor;
$("s_bordercolor").style.backgroundColor = sBorderColor;
$("d_bgcolor").value = sBgColor;
$("s_bgcolor").style.backgroundColor = sBgColor;
$("d_check").checked = bCheck;
$("d_check2").checked = bCheck2;
if(sWordBreak=="break-all")
{
$("d_wordbreak").checked=true;
}
else
{
$("d_wordbreak").checked=false;
}
$("d_position").value = sPosition;
$("d_left").value = sLeft;
$("d_top").value = sTop;
$("d_sborderstyle").value=sBorderStyle;
$("d_zindex").value=sZindex;
$("d_overflow").value=sOverFlow;
}
function SearchSelectValue(o_Select, s_Value){
for (var i=0;i<o_Select.length;i++){
if (o_Select.options[i].value == s_Value){
o_Select.selectedIndex = i;
return true;
}
}
return false;
}
function IsColor(color){
var temp=color;
if (temp=="") return true;
if (temp.length!=7) return false;
return (temp.search(/\#[a-fA-F0-9]{6}/) != -1);
}
function IsDigit(){
return ((event.keyCode >= 48) && (event.keyCode <= 57));
}
function MoreThanOne(obj, sErr){
var b=false;
if (obj.value!=""){
obj.value=parseFloat(obj.value);
if (obj.value!="0"){
b=true;
}
}
if (b==false){
alert(sErr);
return false;
}
return true;
}
function insetDiv()
{
sBorderColor = $("d_bordercolor").value;
if (!IsColor(sBorderColor)&is_ie){
alert('<%=ResourceManager.GetString("errorbordercolorcode")%>');
return;
}
sBgColor = $("d_bgcolor").value;
if (!IsColor(sBgColor)&is_ie){
alert('<%=ResourceManager.GetString("errorbgcolorcode")%>');
return;
}
if ($("d_border").value == "") $("d_border").value = "0";
if ($("d_left").value == "") $("d_left").value = "0";
if ($("d_top").value == "") $("d_top").value = "0";
$("d_border").value = parseFloat($("d_border").value);
$("d_left").value = parseFloat($("d_left").value);
$("d_top").value = parseFloat($("d_top").value);
var sWidth = "";
if ($("d_check").checked){
if (!MoreThanOne($("d_widthvalue"),'<%=ResourceManager.GetString("errorwidth")%>')) return;
sWidth = $("d_widthvalue").value + $("d_widthunit").value;
}
var sHeight = "";
if ($("d_check2").checked){
if (!MoreThanOne($("d_heightvalue"),'<%=ResourceManager.GetString("errorwidth")%>')) return;
sHeight = $("d_heightvalue").value + $("d_heightunit").value;
}
sAlign = $("d_align").options[$("d_align").selectedIndex].value;
sBorder = $("d_border").value;
sPosition = $("d_position").value;
sLeft = $("d_left").value;
sTop = $("d_top").value;
sBorderStyle=$("d_sborderstyle").value;
sZindex=$("d_zindex").value;
sOverFlow=$("d_overflow").value;
if($("d_wordbreak").checked)
{
sWordBreak="break-all";
}
else
{
sWordBreak="normal";
}
if (sAction == "MOD") {
var oControl= new Array;
try {
oControl[0] = sWidth;
}
catch(e)
{
}
try {
oControl[1] = sHeight;
}
catch(e)
{
}
oControl[2]= sAlign;
oControl[3]= sBorder;
oControl[4]= sBgColor;
oControl[5]= sBorderColor;
oControl[6]= sWordBreak;
oControl[7]= sPosition;
oControl[8]= sLeft;
oControl[9]= sTop;
oControl[10]= sZindex;
oControl[11]= sBorderStyle;
oControl[12]= sOverFlow;
if(is_ie){
window.returnValue = oControl;
}
else
{
window.opener.inserObject(null,'moddiv',oControl);
}
window.close();
}
else{
if(is_ie){
var sTable = "<div align='"+sAlign+"' style='border:"+sBorder+"px;width:"+sWidth+";height:"+sHeight+";background-color:"+sBgColor+";border-color:"+sBorderColor+";border-style:"+sBorderStyle+";position:"+sPosition+";overflow:"+sOverFlow+";word-break:"+sWordBreak+";left:"+sLeft+"px;top:"+sTop+"px;z-index:"+sZindex+";'></div>";
window.returnValue = sTable;
}
else{
var sTable = "<div align='"+sAlign+"' style='border:"+sBorder+"px;width:"+sWidth+";height:"+sHeight+";background-color:"+sBgColor+";border-color:"+sBorderColor+";border-style:"+sBorderStyle+";position:"+sPosition+";overflow:"+sOverFlow+";word-break:"+sWordBreak+";left:"+sLeft+"px;top:"+sTop+"px;z-index:"+sZindex+";'>Content...</div>";
window.opener.inserObject(null,'div',sTable);
}
window.close();
}
}
window.focus();
</script>
</head>
<body onload="InitDocument()">
<table border="0" cellpadding="0" cellspacing="0" align="center">
<tr>
<td>
</td>
</tr>
<tr><td height="5"></td>
</tr>
<tr>
<td>
<fieldset>
<legend><span style="color: dimgray"><%=ResourceManager.GetString("divdesign")%></span></legend>
<table border="0"  cellpadding="0" cellspacing="0">
<tr>
<td style="width: 7px"></td>
<td colspan="8" style="height: 30px">
Top:
<input id="d_top" maxlength="4" onkeypress="event.returnValue=IsDigit();" style="width: 35px"
type="text" value="0" />&nbsp; Left:
<input id="d_left" maxlength="4" onkeypress="event.returnValue=IsDigit();" style="width: 35px"
type="text" value="0" />&nbsp;
<%=ResourceManager.GetString("zindex")%>
:
<input id="d_zindex" maxlength="3" onkeypress="event.returnValue=IsDigit();" style="width: 40px"
type="text" />&nbsp;
<%=ResourceManager.GetString("bordersize")%>
:
<input id="d_border" maxlength="3" onkeypress="event.returnValue=IsDigit();" style="width: 40px"
type="text" /></td>
</tr>
<tr>
<td style="width: 7px;"></td>
<td colspan="8" style="height: 30px">
<%=ResourceManager.GetString("position")%>:
<select id="d_position" style="width:72px">
<option value='static'><%=ResourceManager.GetString("static")%></option>
<option value='relative' selected=selected><%=ResourceManager.GetString("relative")%></option>
<option value='absolute'><%=ResourceManager.GetString("absolute")%></option>
</select>
&nbsp;
<%=ResourceManager.GetString("tablealign")%>:
<select id="d_align" style="width:60px">
<option value=''>
<%=ResourceManager.GetString("default")%>
</option>
<option value='left'>
<%=ResourceManager.GetString("left")%>
</option>
<option value='center'>
<%=ResourceManager.GetString("center")%>
</option>
<option value='right'>
<%=ResourceManager.GetString("right")%>
</option>
</select>
&nbsp;
<%=ResourceManager.GetString("borderstyle")%>:
<select id="d_sborderstyle" style="width:50px">
<option value='none'>
<%=ResourceManager.GetString("none")%>
</option>
<option value='solid'>
<%=ResourceManager.GetString("solid")%>
</option>
<option value='dashed'>
<%=ResourceManager.GetString("dashed")%>
</option>
<option value='dotted'>
<%=ResourceManager.GetString("dotted")%>
<option value='double'>
<%=ResourceManager.GetString("double")%>
<option value='groove'>
<%=ResourceManager.GetString("groove")%>
<option value='outset'>
<%=ResourceManager.GetString("ridge")%>
<option value='inset'>
<%=ResourceManager.GetString("inset")%>
<option value='outset'>
<%=ResourceManager.GetString("outset")%>
</option>
</select>
</td>
</tr>
<tr>
<td style="width: 7px;"></td>
<td colspan="8" style="height: 30px">
<%=ResourceManager.GetString("overflow")%>:
<select id="d_overflow" style="width: 80px">
<option value="auto">
<%=ResourceManager.GetString("auto")%>
</option>
<option value="hidden">
<%=ResourceManager.GetString("hidden")%>
</option>
<option value="scroll">
<%=ResourceManager.GetString("scroll")%>
</option>
<option value="visible">
<%=ResourceManager.GetString("visible")%>
</option>
</select>
&nbsp;&nbsp;&nbsp;<input id="d_wordbreak" type="checkbox"/>
<%=ResourceManager.GetString("wordbreak")%>
</td>
</tr>
<tr><td colspan="9" style="height: 5px"></td>
</tr>
</table>
</fieldset>
</td>
</tr>
<tr><td height="5"></td>
</tr>
<tr>
<td>
<fieldset>
<legend><span style="color: dimgray"><%=ResourceManager.GetString("divwidth")%></span></legend>
<table border="0" cellpadding="0" cellspacing="0" width='100%'>
<tr><td colspan="9" height="5"></td>
</tr>
<tr>
<td width="7" style="height: 21px"></td>
<td onclick="d_check.click()" nowrap="nowrap" valign="middle" style="height: 21px"><input id="d_check" type="checkbox" onclick="d_widthvalue.disabled=(!this.checked);d_widthunit.disabled=(!this.checked);" value="1" />
<%=ResourceManager.GetString("divwidthvalue")%></td>
<td align="right" width="60%" style="height: 21px">
<input id="d_widthvalue" type="text" value="" size="5" onkeypress="event.returnValue=IsDigit();" maxlength="4" />
<select id="d_widthunit">
<option value='px'><%=ResourceManager.GetString("tablepx")%></option><option value='%'><%=ResourceManager.GetString("tablepercent")%></option>
</select>
</td>
<td width="7" style="height: 21px"></td>
</tr>
<tr><td colspan="9" height="5"></td>
</tr>
</table>
</fieldset>
<fieldset>
<legend><span style="color: dimgray"><%=ResourceManager.GetString("divheight")%></span></legend>
<table border="0" cellpadding="0" cellspacing="0" width='100%'>
<tr><td colspan="9" height="5"></td>
</tr>
<tr>
<td width="7" style="height: 21px"></td>
<td onclick="d_check2.click()" nowrap="nowrap" valign="middle" style="height: 21px"><input id="d_check2" type="checkbox" onclick="d_heightvalue.disabled=(!this.checked);d_heightunit.disabled=(!this.checked);" value="1" />
<%=ResourceManager.GetString("divheightvalue")%></td>
<td align="right" width="60%" style="height: 21px">
<input id="d_heightvalue" type="text" value="" size="5" onkeypress="event.returnValue=IsDigit();" maxlength="4" />
<select id="d_heightunit">
<option value='px'><%=ResourceManager.GetString("tablepx")%></option><option value='%'><%=ResourceManager.GetString("tablepercent")%></option>
</select>
</td>
<td width="7" style="height: 21px"></td>
</tr>
<tr><td colspan="9" height="5"></td>
</tr>
</table>
</fieldset>
</td>
</tr>
<tr><td height="5"></td>
</tr>
<tr>
<td>
<fieldset>
<legend><span style="color: dimgray"><%=ResourceManager.GetString("divcolor")%></span></legend>
<table border="0" cellpadding="0" cellspacing="0">
<tr><td colspan="9" height="5"></td>
</tr>
<tr>
<td width="7"></td>
<td><%=ResourceManager.GetString("tablebordercolor")%>:</td>
<td width="5"></td>
<td><input type="text" id="d_bordercolor" onblur='s_bordercolor.style.backgroundColor=this.value' size="7" value="" /></td>
<td style="width: 23px">
&nbsp;<img border="0" src="img/selcolor.gif" width="18" style="cursor:pointer" title="<%=ResourceManager.GetString("tableselectbordercolor")%>" id="s_bordercolor" onclick="colordialog('bordercolor')" /></td>
<td width="40"></td>
<td><%=ResourceManager.GetString("tablebgcolor")%>::</td>
<td width="5"></td>
<td><input type="text" id="d_bgcolor" onblur='s_bgcolor.style.backgroundColor=this.value' size="7" value="" /></td>
<td>
&nbsp;<img border="0" src="img/selcolor.gif" width="18" style="cursor:pointer" title="<%=ResourceManager.GetString("tableselectbgcolor")%>" id="s_bgcolor" onclick="colordialog('bgcolor')" /></td>
<td width="7"></td>
</tr>
<tr><td colspan="9" height="5"></td>
</tr>
</table>
</fieldset>
</td>
</tr>
<tr><td height="5"></td>
</tr>
<tr><td align="center">
<button style="width: 76px" onclick="insetDiv()"><%=ResourceManager.GetString("ok")%></button>
&nbsp;<button onclick="window.close();" style="width: 76px"><%=ResourceManager.GetString("close2")%></button></td></tr>
</table>
<script language="JavaScript" type="text/javascript">
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

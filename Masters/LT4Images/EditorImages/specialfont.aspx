<%@ Page language="c#" AutoEventWireup="true"  CodePage="936"  Inherits="LongTrueComEditor.PageCode"%>
<%@ Import Namespace="LongTrueComEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title><%=ResourceManager.GetString("symbol")%></title>
<meta http-equiv="pragma" content="no-cache" />
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<base target="_self" />
<script type="text/javascript">
var userAgent = navigator.userAgent.toLowerCase();
var is_ie = (userAgent.indexOf('msie') != -1);
var is_ie7 = (userAgent.indexOf('msie 7.') != -1);
var is_moz = (navigator.product == 'Gecko');

Request = {
 QueryString : function(item){
  var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)","i"));
  return svalue ? svalue[1] : svalue;
 }
}
function crfont(picid)
{
    parent.inserObject(parent.document.getElementById(Request.QueryString("name")).contentWindow,'symbol',picid);
����parent.popupmenu.hide();
����parent.rcmenu.hide();
}

    function yulan(picid){
    document.getElementById("font_yulan").innerHTML=picid;
    }
    function sel(value)
    {
    huifu()
    document.getElementById(value).className="selface_bq_b";
    document.getElementById(value+"photo").style.display="block";
    slef=number;
    }
    function unsel(value)
    {
    if(slef==value){document.getElementById(value).className="selface_bq_b";}
    }
if(is_ie&&!is_ie7)
{
document.write("<style>.selfont_eve {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px}.selface_yulan {BORDER-LEFT-COLOR: #006666; LEFT: 13px; BORDER-BOTTOM-COLOR: #006666; OVERFLOW: hidden; WIDTH: 50px; BORDER-TOP-STYLE: double; BORDER-TOP-COLOR: #006666; BORDER-RIGHT-STYLE: double; BORDER-LEFT-STYLE: double; POSITION: absolute; TOP: 147px; HEIGHT: 50px; BACKGROUND-COLOR: #ffffff; BORDER-RIGHT-COLOR: #006666; BORDER-BOTTOM-STYLE: double}</style>");   
}
else
{
if(is_moz)
{
document.write("<style>.selfont_eve {PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px}.selface_yulan {BORDER-LEFT-COLOR: #006666; LEFT: 13px; BORDER-BOTTOM-COLOR: #006666; OVERFLOW: hidden; WIDTH: 50px; BORDER-TOP-STYLE: double; BORDER-TOP-COLOR: #006666; BORDER-RIGHT-STYLE: double; BORDER-LEFT-STYLE: double; POSITION: absolute; TOP: 155px; HEIGHT: 50px; BACKGROUND-COLOR: #ffffff; BORDER-RIGHT-COLOR: #006666; BORDER-BOTTOM-STYLE: double}</style>");  
}
else
{
document.write("<style>.selfont_eve {PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px}.selface_yulan {BORDER-LEFT-COLOR: #006666; LEFT: 13px; BORDER-BOTTOM-COLOR: #006666; OVERFLOW: hidden; WIDTH: 50px; BORDER-TOP-STYLE: double; BORDER-TOP-COLOR: #006666; BORDER-RIGHT-STYLE: double; BORDER-LEFT-STYLE: double; POSITION: absolute; TOP: 142px; HEIGHT: 50px; BACKGROUND-COLOR: #ffffff; BORDER-RIGHT-COLOR: #006666; BORDER-BOTTOM-STYLE: double}</style>");
}
}

</script>
<style type="text/css">
body,a,table,input,select{
MARGIN: 0px 0px 0px 1px;font-size:12px;font-family:����,Verdana,Arial
}
.lfd {
	FLOAT: left
}
.selface_kong1 {
	MARGIN-TOP: 3px; WIDTH: 1px; HEIGHT: 6px
}
.selface_02
{
	border-right: #999999 1px solid;
	border-top: #999999 1px solid;
	border-left: #999999 1px solid;
	border-bottom: #999999 1px solid;
	width: 300px;
	height: 190px;
	background-color: #ffffff;
	margin-top: 8px;
	margin-left: 65px;
	left: 8px;
	z-index:-1;
	position:absolute;
}
.selface_bq_a {
	BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; MARGIN-TOP: 1px; MARGIN-LEFT: 11px; BORDER-LEFT: #999999 1px solid; WIDTH: 60px; cursor:pointer; COLOR: #666633; PADDING-TOP: 5px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 18px; BACKGROUND-COLOR: #cccccc; TEXT-ALIGN: center
}
.selface_bq_b {
	BORDER-RIGHT: #ffffff 1px solid; BORDER-TOP: #999999 1px solid; MARGIN-TOP: 1px; MARGIN-LEFT: 11px; BORDER-LEFT: #999999 1px solid; WIDTH: 60px; cursor:pointer; COLOR: #330033; PADDING-TOP: 5px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 18px; BACKGROUND-COLOR: #ffffff; TEXT-ALIGN: center
}
.selface_close {
MARGIN-TOP: 3px;MARGIN-RIGHT:5px
}

.selftface_xs {
	MARGIN-TOP: 15px; MARGIN-LEFT: 50px; WIDTH: 300px
}
.selfont {
	VERTICAL-ALIGN:middle; WIDTH: 8px; cursor:pointer; HEIGHT: 8px; TEXT-ALIGN: center
}

#font_yulan {
	FONT-SIZE: 23pt
}
.pic_up_0 {
	MARGIN-TOP: 10px; MARGIN-LEFT: 8px; WIDTH: 240px; COLOR: #333333
}
</style>
</head>
<body topmargin="0" bgcolor="#f9f8f7">
<form>
<div class="lfd">
<div class="selface_kong1"></div>
<div class="selface_bq_a" onmouseover="sel('special_symbol')" onmouseout="unsel('special_symbol')" id="special_symbol"><%=ResourceManager.GetString("special_symbol")%> </div>
<div class="selface_bq_a" onmouseover="sel('number')" onmouseout="unsel('number')" id="number"><%=ResourceManager.GetString("number")%> </div>
<div class="selface_bq_a" onmouseover="sel('spellline')" onmouseout="unsel('spellline')" id="spellline"><%=ResourceManager.GetString("spellline")%> </div>
<div class="selface_bq_a" onmouseover="sel('othersymbol1')" onmouseout="unsel('othersymbol1')" id="othersymbol1"><%=ResourceManager.GetString("othersymbol1")%> </div>
<div class="selface_bq_a" onmouseover="sel('othersymbol2')" onmouseout="unsel('othersymbol2')"  id="othersymbol2"><%=ResourceManager.GetString("othersymbol2")%> </div>
<div class="selface_yulan">
<table id="table1" height="100%" cellspacing="0" cellpadding="0" width="100%" 
border="0">
  <tbody>
  <tr>
    <td align="center" valign="middle"><div id="font_yulan"></div></td></tr></tbody></table></div></div>
<div class="selface_02"></div>
<div id="special_symbolphoto" style="display:block">
<div class="selftface_xs">
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�@')" onclick="crfont('�@')">�@</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�A')" onclick="crfont('�A')">�A</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�B')" onclick="crfont('�B')">�B</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�C')" onclick="crfont('�C')">�C</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�D')" onclick="crfont('�D')">�D</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�E')" onclick="crfont('�E')">�E</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�F')" onclick="crfont('�F')">�F</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�G')" onclick="crfont('�G')">�G</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�H')" onclick="crfont('�H')">�H</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�I')" onclick="crfont('�I')">�I</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�J')" onclick="crfont('�J')">�J</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�K')" onclick="crfont('�K')">�K</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�L')" onclick="crfont('�L')">�L</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�M')" onclick="crfont('�M')">�M</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�N')" onclick="crfont('�N')">�N</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�O')" onclick="crfont('�O')">�O</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�P')" onclick="crfont('�P')">�P</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�Q')" onclick="crfont('�Q')">�Q</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�R')" onclick="crfont('�R')">�R</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�S')" onclick="crfont('�S')">�S</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�T')" onclick="crfont('�T')">�T</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�U')" onclick="crfont('�U')">�U</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�V')" onclick="crfont('�V')">�V</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�W')" onclick="crfont('�W')">�W</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�Y')" onclick="crfont('�Y')">�Y</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�Z')" onclick="crfont('�Z')">�Z</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�\')" onclick="crfont('�\')">�\</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�`')" onclick="crfont('�`')">�`</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�a')" onclick="crfont('�a')">�a</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�b')" onclick="crfont('�b')">�b</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�c')" onclick="crfont('�c')">�c</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�d')" onclick="crfont('�d')">�d</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�e')" onclick="crfont('�e')">�e</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�f')" onclick="crfont('�f')">�f</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�g')" onclick="crfont('�g')">�g</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�h')" onclick="crfont('�h')">�h</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�i')" onclick="crfont('�i')">�i</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�j')" onclick="crfont('�j')">�j</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�k')" onclick="crfont('�k')">�k</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�l')" onclick="crfont('�l')">�l</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�m')" onclick="crfont('�m')">�m</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�n')" onclick="crfont('�n')">�n</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�')" onclick="crfont('�')">�</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('?')" onclick="crfont('?')">&#8482;</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('?')" onclick="crfont('?')">&reg;</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('?')" 
onclick="crfont('?')">&copy;</div></div></div></div>
<div id="numberphoto" style="DISPLAY: none">
<div class="selftface_xs">
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" 
onclick="crfont('��')">��</div></div></div></div>
<div id="spelllinephoto" style="DISPLAY: none">
<div class="selftface_xs">
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�T')" onclick="crfont('�T')">�T</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�U')" onclick="crfont('�U')">�U</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�V')" onclick="crfont('�V')">�V</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�W')" onclick="crfont('�W')">�W</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�X')" onclick="crfont('�X')">�X</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�Y')" onclick="crfont('�Y')">�Y</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�Z')" onclick="crfont('�Z')">�Z</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�[')" onclick="crfont('�[')">�[</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�\')" onclick="crfont('�\')">�\</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�]')" onclick="crfont('�]')">�]</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�^')" onclick="crfont('�^')">�^</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�_')" onclick="crfont('�_')">�_</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�`')" onclick="crfont('�`')">�`</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�a')" onclick="crfont('�a')">�a</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�b')" onclick="crfont('�b')">�b</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�c')" onclick="crfont('�c')">�c</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�d')" onclick="crfont('�d')">�d</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�e')" onclick="crfont('�e')">�e</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�f')" onclick="crfont('�f')">�f</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�g')" onclick="crfont('�g')">�g</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�h')" onclick="crfont('�h')">�h</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�i')" onclick="crfont('�i')">�i</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�j')" onclick="crfont('�j')">�j</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�k')" onclick="crfont('�k')">�k</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�l')" onclick="crfont('�l')">�l</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�m')" onclick="crfont('�m')">�m</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�n')" onclick="crfont('�n')">�n</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�o')" onclick="crfont('�o')">�o</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�p')" 
onclick="crfont('�p')">�p</div></div></div></div>
<div id="othersymbol1photo" style="DISPLAY: none">
<div class="selftface_xs">
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" 
onclick="crfont('��')">��</div></div></div></div>
<div id="othersymbol2photo" style="DISPLAY: none">
<div class="selftface_xs">
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�@')" onclick="crfont('�@')">�@</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�A')" onclick="crfont('�A')">�A</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�B')" onclick="crfont('�B')">�B</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�C')" onclick="crfont('�C')">�C</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�D')" onclick="crfont('�D')">�D</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�E')" onclick="crfont('�E')">�E</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�F')" onclick="crfont('�F')">�F</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�G')" onclick="crfont('�G')">�G</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�H')" onclick="crfont('�H')">�H</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�u')" onclick="crfont('�u')">�u</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�v')" onclick="crfont('�v')">�v</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�w')" onclick="crfont('�w')">�w</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�x')" onclick="crfont('�x')">�x</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�y')" onclick="crfont('�y')">�y</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�z')" onclick="crfont('�z')">�z</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�{')" onclick="crfont('�{')">�{</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�|')" onclick="crfont('�|')">�|</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�}')" onclick="crfont('�}')">�}</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�~')" onclick="crfont('�~')">�~</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�o')" onclick="crfont('�o')">�o</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�p')" onclick="crfont('�p')">�p</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�q')" onclick="crfont('�q')">�q</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�r')" onclick="crfont('�r')">�r</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�s')" onclick="crfont('�s')">�s</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�t')" onclick="crfont('�t')">�t</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�u')" onclick="crfont('�u')">�u</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�v')" onclick="crfont('�v')">�v</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�w')" onclick="crfont('�w')">�w</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�x')" onclick="crfont('�x')">�x</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�y')" onclick="crfont('�y')">�y</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�z')" onclick="crfont('�z')">�z</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�{')" onclick="crfont('�{')">�{</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�|')" onclick="crfont('�|')">�|</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�}')" onclick="crfont('�}')">�}</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('�~')" onclick="crfont('�~')">�~</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('?')" onclick="crfont('?')">?</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" onclick="crfont('��')">��</div></div>
<div class="selfont_eve lfd">
<div class="selfont" onmouseover="yulan('��')" 
onclick="crfont('��')">��</div></div></div></div>
</form>
<script language="JavaScript" type="text/javascript">
    document.getElementById("special_symbol").className="selface_bq_b";
    var slef;
    function huifu(){
    document.getElementById("special_symbol").className="selface_bq_a";
    document.getElementById("number").className="selface_bq_a";
    document.getElementById("spellline").className="selface_bq_a";
    document.getElementById("othersymbol1").className="selface_bq_a";
    document.getElementById("othersymbol2").className="selface_bq_a";
    document.getElementById("special_symbolphoto").style.display="none";
    document.getElementById("numberphoto").style.display="none";
    document.getElementById("spelllinephoto").style.display="none";
    document.getElementById("othersymbol1photo").style.display="none";
    document.getElementById("othersymbol2photo").style.display="none";
    }
</script>
</body></html>
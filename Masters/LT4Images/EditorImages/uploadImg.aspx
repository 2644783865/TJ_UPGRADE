<%@ Page Language="C#" AutoEventWireup="true" Inherits="LongTrueComEditor.UpLoad" %>
<%@ Import Namespace="LongTrueComEditor" %>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<base target="_self" />
<script language="javascript" type="text/javascript">
var userAgent = navigator.userAgent.toLowerCase();
var is_ie = (userAgent.indexOf('msie') != -1);
var image = new Array;
var w,h;
function changeWaterMark()
{
if(document.getElementById("watermark").checked)
{
document.getElementById("watermarkimginput").disabled=false;
document.getElementById("imgx").disabled=false;
document.getElementById("imgy").disabled=false;
document.getElementById("config_watermarkImages").value="true";
}
else
{
document.getElementById("watermarkimginput").disabled=true;
document.getElementById("imgx").disabled=true;
document.getElementById("imgy").disabled=true;
document.getElementById("config_watermarkImages").value="false";
}
if(document.getElementById("watermarkText").checked)
{
document.getElementById("watermarktextinput").disabled=false;
document.getElementById("textx").disabled=false;
document.getElementById("texty").disabled=false;
document.getElementById("textsize").disabled=false;
document.getElementById("fontcolor").disabled=false;
document.getElementById("fonttype").disabled=false;
document.getElementById("config_watermark").value="true";
}
else
{
document.getElementById("watermarktextinput").disabled=true;
document.getElementById("textx").disabled=true;
document.getElementById("texty").disabled=true;
document.getElementById("textsize").disabled=true;
document.getElementById("fontcolor").disabled=true;
document.getElementById("fonttype").disabled=true;
document.getElementById("config_watermark").value="false";
}
}
function loading(showmessage)
{
document.getElementById("loading").style.visibility="visible";
document.getElementById("statusmessage").innerHTML=showmessage;
return true;
}
function checksize(str,type)
{
if(type=="wedth"&document.getElementById("ImgWidth").value!=""&document.getElementById("ImgHeight").value!="")
{
if(w!=null&&h!=null)
{
document.getElementById("ImgHeight").value=Math.round((parseInt(str)*h)/w);
}
else
{
w=str;
h=document.getElementById("ImgHeight").value;
}
}
else if(document.getElementById("ImgWidth").value!=""&document.getElementById("ImgHeight").value!="")
{
if(w!=null&&h!=null)
{
document.getElementById("ImgWidth").value=Math.round((parseInt(str)*w)/h);
}
else
{
w=document.getElementById("ImgWidth").value;
h=str;
}
}
}
function preview(name)
{
if(is_ie)
{
var path=document.getElementById("path").innerText;
}
else
{
var path=document.getElementById("path").textContent;
}
document.getElementById("file_path").value=path+name;
if(document.getElementById("ImgWidth"))
{
document.getElementById("previewImg").innerHTML='<img src='+document.getElementById("file_path").value.replace(/\s/g,"\%20")+' align="middle" onload="ImgWidth.value=this.width;ImgHeight.value=this.height;w=this.width;h=this.height;if(this.width>300){this.width=300;this.height=Math.round(this.width*h/w);}if(this.height>225){this.height=225;this.width=Math.round(this.height*w/h);}" />';
}
else
{
document.getElementById("previewImg").innerHTML='<img src='+document.getElementById("file_path").value.replace(/\s/g,"\%20")+' align="middle" onload="w=this.width;h=this.height;if(this.width>300){this.width=300;this.height=Math.round(this.width*h/w);}if(this.height>225){this.height=225;this.width=Math.round(this.height*w/h);}" />';
}
document.getElementById("file_path").focus();
}
function newImages()
{
if(document.getElementById("file_path").value!="")
{
var arr=new Array;
arr[0]=document.getElementById("file_path").value.replace(/\s/g,"\%20");
arr[1]=document.getElementById("ImgWidth")?document.getElementById("ImgWidth").value:"";
arr[2]=document.getElementById("ImgHeight")?document.getElementById("ImgHeight").value:"";
arr[3]=document.getElementById("ImgAlt")?document.getElementById("ImgAlt").value:"";
arr[4]=document.getElementById("Imgalign")?document.getElementById("Imgalign").value:"";
arr[5]=document.getElementById("vspace")?document.getElementById("vspace").value:"";
arr[6]=document.getElementById("hspace")?document.getElementById("hspace").value:"";
if(is_ie)
{
window.returnValue = arr;
}
else
{
if(document.getElementById("insertImg").value!='<%=ResourceManager.GetString("mof")%>')
{
window.opener.inserObject(null,'img',arr);
}
else
{
window.opener.inserObject(null,'modimg',arr);
}
}
}
window.close();
}
var sTitle='<%=ResourceManager.GetString("insertimages")%>';
if(is_ie)
{
if (dialogArguments!=null)
sTitle='<%=ResourceManager.GetString("mofimages")%>';
}
else
{
image=window.opener.GetImg();
if(image[0]!=null)
{
sTitle='<%=ResourceManager.GetString("mofimages")%>';
}
window.focus();
}
document.write("<TITLE>" + sTitle + "</TITLE>");
</script>
<link href="stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0">
<form id="uploadFace" runat="server">
<table border="0" align="center" style="word-break:break-all" width="100%">
<tr>
<td colspan="4" rowspan="1" valign="top" style="width: 840px">
<fieldset><legend><span style="color: gray"><%=ResourceManager.GetString("uploadface")%></span>&nbsp;</legend>
<%=ResourceManager.GetString("uploadpath")%>：<asp:Label ID="path" runat="server" ForeColor="Black"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="returndir" OnClick="returndir_Click" ImageUrl="img/parentfolder.gif" runat=server/>..<br />
<%=ResourceManager.GetString("uploadimages")%>：<asp:FileUpload ID="FileUpload1" runat="server" Width="388px" Height="21px" TabIndex="2" Font-Size="10pt" /><asp:Button ID="uploadBtn" runat="server" OnClick="UploadBtn_Click"/><asp:TextBox ID="remoteurl" runat="server" Visible="False" Text="http://" Width="350px"></asp:TextBox>
<asp:Button ID="remoteupload" runat="server" Visible="False" Width="49px" OnClick="remoteupload_Click" /><br />
<%=ResourceManager.GetString("filepath")%>：<asp:TextBox ID="file_path" runat="server" Width="316px" TabIndex="1"></asp:TextBox>
<input language="javascript" onclick="javascript:newImages()" type="button" value='<%=ResourceManager.GetString("insertimage")%>' id="insertImg" /><br />
<asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
<asp:ListItem Selected=True Value="local" />
<asp:ListItem Value="remote" />
</asp:RadioButtonList>[ <%=ResourceManager.GetString("uploaduse")%>：<asp:label ID="useSpace" ForeColor="Red" runat=server />，<%=ResourceManager.GetString("have")%>：<asp:label ID="space" ForeColor="Red" runat=server /><%=ResourceManager.GetString("singlesize")%>：<asp:Label ID="maxSingleUploadSize" runat="server" ForeColor="Red"></asp:Label>
]</fieldset>
<fieldset style="text-align: center"><legend><span style="color: gray"><%=ResourceManager.GetString("filelist")%></span>&nbsp;</legend>
<div style="border-right: 1.5pt inset; border-top: 1.5pt inset; vertical-align: middle;
overflow: auto; border-left: 1.5pt inset; width: 100%; border-bottom: 1.5pt inset;
height: 240px; background-color: white">
<asp:GridView runat="server" id="File_List" HeaderStyle-HorizontalAlign=Center AutoGenerateColumns="False" HeaderStyle-BackColor="buttonface" HeaderStyle-ForeColor=windowtext HeaderStyle-Font-Bold="True" Width="100%" BorderWidth="1px" OnRowCancelingEdit="File_List_RowCancelingEdit" OnRowUpdating="File_List_RowUpdating">
<Columns>
<asp:TemplateField>
<HeaderTemplate>
<asp:CheckBox ID="checkall" runat="server" Text=<%#ResourceManager.GetString("selectall")%> AutoPostBack="true" OnCheckedChanged="checkAll" />
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="check" runat="server" />
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" Width="45px" />
</asp:TemplateField>
<asp:TemplateField>
<EditItemTemplate>
<asp:TextBox ID="editName" Text=<%#DataBinder.Eval(Container.DataItem,"Attributes").ToString().ToLower()=="directory"?DataBinder.Eval(Container.DataItem,"Name"):DataBinder.Eval(Container.DataItem,"Name").ToString().Replace(DataBinder.Eval(Container.DataItem,"Extension").ToString(),string.Empty)%> Width="100px" runat=server></asp:TextBox> <asp:Button ID="editBtn" CommandName="Update" CommandArgument=<%#DataBinder.Eval(Container.DataItem,"Name")%> runat=server Text=<%#ResourceManager.GetString("edit")%> /> <asp:Button ID="Cancel" runat=server Text=<%#ResourceManager.GetString("cancel")%> CommandArgument=<%#DataBinder.Eval(Container.DataItem,"Attributes").ToString().ToLower()%> CommandName="Cancel" />
</EditItemTemplate>
<ItemTemplate>
<img src="img/filetype/<%#DataBinder.Eval(Container.DataItem,"Attributes").ToString().ToLower()=="directory"?"folder":((string)DataBinder.Eval(Container.DataItem,"Extension")).Replace(".","")%>.gif" /><asp:LinkButton ID="ListID" Text=<%#DataBinder.Eval(Container.DataItem,"Name")%> style="cursor:pointer; word-break:break-all" ForeColor="#000000" Font-Underline=false onmouseout="this.style.textDecoration='none'" onmouseover="this.style.textDecoration='underline'" CommandArgument=<%#DataBinder.Eval(Container.DataItem,"Name").ToString()%> OnCommand="SetServerCookie" OnClientClick=<%#DataBinder.Eval(Container.DataItem,"Attributes").ToString().ToLower()!="directory"?DataBinder.Eval(Container.DataItem,"Name","javascript:preview(\"{0}\");return false;"):""%> runat="server"/>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="LastWriteTime" ReadOnly="True" HtmlEncode=False DataFormatString="{0:D}" >
<ItemStyle HorizontalAlign="Center" Width="25%" />
</asp:BoundField>
<asp:TemplateField>
<ItemTemplate>
<asp:Label ID="LengthCont" Text=<%#DataBinder.Eval(Container.DataItem,"Attributes").ToString().ToLower()=="directory"?"":DataBinder.Eval(Container.DataItem,"Length","{0:#,### Bytes}")%> runat=server />
</ItemTemplate>
<ItemStyle HorizontalAlign=Center Width="25%" />
</asp:TemplateField>
</Columns>
<HeaderStyle Font-Bold="True" ForeColor="WindowText" BackColor="Control" BorderWidth="1px" HorizontalAlign="Center" />
</asp:GridView></div>
<table width="100%" border="0px"><tr><td valign="baseline" style="height: 23px">
[<%=ResourceManager.GetString("controlmenu")%>]：<asp:ImageButton id="selectAllBtn" runat="server" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" ImageUrl="img/selectall.gif" onclick="selectAllBtn_Click" />&nbsp;
<asp:ImageButton ID="deleteBtn" runat="server" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" ImageUrl="img/delete.gif" OnClick="deleteBtn_Click" />&nbsp;&nbsp;<asp:ImageButton id="editBtn" ImageUrl="img/rename.gif" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" runat="server" onclick="editBtn_Click" />&nbsp;&nbsp;<asp:ImageButton ID="newfolderBtn" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" runat="server" ImageUrl="img/newfolder.gif" OnClick="newfolderBtn_Click" />&nbsp;&nbsp;<asp:ImageButton ID="returndir2" OnClick="returndir_Click" ImageUrl="img/parentfolder.gif" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" runat=server/>&nbsp;&nbsp;<input language="javascript" onmouseup="if(is_ie){showModalDialog('find.aspx',this,'dialogWidth:320px;dialogHeight:130px;status:0;scroll:no');}else{window.find();}"
type=image src="img/search.gif" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" title='<%=ResourceManager.GetString("findfile")%>' />&nbsp;
<input language="javascript" onclick="window.close();" type="image" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" src="img/close.gif" title="<%=ResourceManager.GetString("close")%>" /></td>
<td align="right" style="height: 23px">
<img border=0px src="img/logo_S.png" /></td>
</tr></table>
<fieldset><legend><span style="color: dimgray"><span style="color: gray"><%=ResourceManager.GetString("imagepreview")%></span>&nbsp;</span></legend>
<table height="100%" width="100%">
<tr>
<td colspan="3" rowspan="3">
<div style="border-right: 1.5pt inset; padding-right: 0px; border-top: 1.5pt inset;
padding-left: 0px; padding-bottom: 0px; vertical-align:middle; overflow: auto;
border-left: 1.5pt inset; width: 320px; padding-top: 2px; border-bottom: 1.5pt inset;
height: 235px; background-color: white">
<div id="previewImg" align="center" style="background-color: white">
</div>
</div></td>
<td colspan="1" rowspan="3" Width="180px" valign="top">
<asp:HiddenField ID="config_watermark" runat="server" />
<asp:HiddenField ID="config_watermarkImages" runat="server" />
<asp:HiddenField ID="config_watermakOption" runat="server" />
<asp:HiddenField ID="config_smallImages" runat="server" />
<asp:HiddenField ID="config_smallImagesName" runat="server" />
<asp:HiddenField ID="config_maxAllUploadSize" runat="server" />
<asp:HiddenField ID="config_autoname" runat="server" />
<asp:HiddenField ID="config_allowUpload" runat="server" />
<asp:HiddenField ID="config_fileFilters" runat="server" />
<asp:HiddenField ID="config_maxSingleUploadSize" runat="server" />
<asp:HiddenField ID="config_fileListBox" runat="server" />
<asp:HiddenField ID="config_watermarkImagesName" runat="server" />
<asp:HiddenField ID="config_watermarkName" runat="server" />
<asp:HiddenField ID="config_smallImagesType" runat="server" />
<asp:HiddenField ID="config_smallImagesW" runat="server" />
<asp:HiddenField ID="config_smallImagesH" runat="server" />
<asp:HiddenField ID="config_type" Value="Images" runat="server" />
<asp:Button ID="settingimg" CommandArgument="0" Enabled=false OnClick="settingimg_Click" runat=server />
<asp:Button ID="settingwatermark" CommandArgument="1" OnClick="settingwatermark_Click" runat=server />
<asp:MultiView id="showsetupface" runat="server" ActiveViewIndex="0">
<asp:View ID="imgageattribute" runat="server" >
<div style="width:150px" align="left"><br />
<%=ResourceManager.GetString("width")%>：<input id="ImgWidth" style="width: 97px" onblur="if(document.getElementById('checkimgsize').checked){checksize(this.value,'wedth');}" type="text" maxlength="10" /><br />
<%=ResourceManager.GetString("height")%>：<input id="ImgHeight" style="width: 97px" type="text" onblur="if(document.getElementById('checkimgsize').checked){checksize(this.value,'height');}" maxlength="10" /><br /><input id="checkimgsize" type="checkbox" checked="CHECKED" />&nbsp;
    <%=ResourceManager.GetString("keepratio")%><br />
<%=ResourceManager.GetString("alt")%>：<input id="ImgAlt" style="width: 97px" type="text" maxlength="100" /><br />
    <br />
    <%=ResourceManager.GetString("align")%>：<select id="Imgalign" style="width: 97px">
<option selected="selected" value=""><%=ResourceManager.GetString("default")%></option>
<option value="left"><%=ResourceManager.GetString("left")%></option>
<option value="center"><%=ResourceManager.GetString("center")%></option>
<option value="right"><%=ResourceManager.GetString("right")%></option>
</select><br /><br />
<%=ResourceManager.GetString("vspace")%>：<input id="vspace" style="width: 75px" type="text" maxlength="3" /><br />
    <br />
<%=ResourceManager.GetString("hspace")%>：<input id="hspace" style="width: 75px" type="text" maxlength="3" /></div>
</asp:View>
<asp:View ID="watermarksetup" runat="server">
<div style="width:180px" align="left">
<br />
<input id="watermark" onclick="changeWaterMark()" type="checkbox" /><%=ResourceManager.GetString("watermark")%>
    <br />
    <%=ResourceManager.GetString("watermarkposition")%>：<%=ResourceManager.GetString("positionx")%>：<asp:TextBox runat=server id="imgx" Enabled=false style="width: 40px" maxlength="4" Text="0" />&nbsp;<%=ResourceManager.GetString("positiony")%>：<asp:TextBox runat=server id="imgy" Enabled=false style="width: 40px" maxlength="4" Text="0" /><br />
    <%=ResourceManager.GetString("photoaddress")%>：<asp:TextBox ID="watermarkimginput" Enabled=false runat="server" Width="105px"/><br />
    <br />
<input id="watermarkText" onclick="changeWaterMark()" type="checkbox" /><%=ResourceManager.GetString("watermarkText")%>
    <br />
    <%=ResourceManager.GetString("watermarkposition")%>：<%=ResourceManager.GetString("positionx")%>：<asp:TextBox runat=server id="textx" Enabled=false style="width: 40px" maxlength="4" Text="15" />&nbsp;<%=ResourceManager.GetString("positiony")%>：<asp:TextBox runat=server id="texty" Enabled=false style="width: 40px" maxlength="4"  Text="15" /><br />
    <%=ResourceManager.GetString("inputfont")%>：<asp:TextBox ID="watermarktextinput" Enabled=false runat="server" Width="130px"/><br />
    <%=ResourceManager.GetString("font")%>：<asp:DropDownList ID="fonttype" runat="server" Width="100px"></asp:DropDownList><br />
    <%=ResourceManager.GetString("fontsize")%>：<asp:TextBox ID="textsize" Enabled=false runat="server" Text="12" MaxLength="3" Width="30px"/>&nbsp;&nbsp;<%=ResourceManager.GetString("inputcolor")%>：<asp:TextBox ID="fontcolor" Enabled=false runat="server" Text="#000000" MaxLength="10" Width="60px"/></div>
    <script language=javascript>
if(document.getElementById("config_watermark").value=="true")
{
document.getElementById("watermarkText").checked=true;
document.getElementById("watermarktextinput").disabled=false;
document.getElementById("textx").disabled=false;
document.getElementById("texty").disabled=false;
document.getElementById("textsize").disabled=false;
document.getElementById("fontcolor").disabled=false;
document.getElementById("fonttype").disabled=false;
}
if(document.getElementById("config_watermarkImages").value=="true")
{
document.getElementById("watermark").checked=true;
document.getElementById("watermarkimginput").disabled=false;
document.getElementById("imgx").disabled=false;
document.getElementById("imgy").disabled=false;
}
    </script></asp:View>
</asp:MultiView>
</td>
</tr>
</table>
</fieldset>
</fieldset>
</td>
</tr>
</table>
<div id="loading" style="border-right: #333333 1px dashed; border-top: #333333 1px dashed;
font-size: 9pt;visibility:hidden; border-left: #333333 1px dashed;
width: 270px; color: #000000; border-bottom: #333333 1px dashed; position: absolute; height: 120px; background-color: #ffffff">
<center>
<br />
<br />
<span id="statusmessage"></span>
</center>
<br />
<center>
<asp:Button ID="canceloading" runat="server" Style="border-top-style: dashed; border-right-style: dashed;
border-left-style: dashed; border-bottom-style: dashed" />&nbsp;</center>
<br />
</div>
<script type="text/javascript">
var load=document.getElementById('loading');
window.onload=function(){resizeLoad()};
function resizeLoad()
{
load.style.top = parseInt((document.body.clientHeight-load.offsetHeight)/2+document.body.scrollTop);
load.style.left = parseInt((document.body.clientWidth-load.offsetWidth)/2+document.body.scrollLeft);
}
if(is_ie)
{
document.body.bgColor="ButtonFace";
if (window.dialogArguments&&document.getElementById("settingimg").disabled)
{
image = dialogArguments;
document.getElementById("ImgWidth").value=image[0];
w=image[0];
h=image[1];
document.getElementById("ImgHeight").value=image[1];
document.getElementById("ImgAlt").value=image[2];
document.getElementById("file_path").value=image[3];
document.getElementById("Imgalign").value=image[4];
document.getElementById("vspace").value=image[5]?image[5]:"";
document.getElementById("hspace").value=image[6]?image[6]:"";
document.getElementById("insertImg").value='<%=ResourceManager.GetString("mof")%>';
document.getElementById("previewImg").innerHTML='<img src='+image[3]+' align="middle" onload="w=this.width;h=this.height;if(this.width>300){this.width=300;this.height=Math.round(this.width*h/w);}if(this.height>225){this.height=225;this.width=Math.round(this.height*w/h);}" />';
}
}
else
{
document.body.bgColor="#E0E0E0";
if(image[0]!=null)
{
document.getElementById("ImgWidth").value=image[0];
document.getElementById("ImgHeight").value=image[1];
document.getElementById("ImgAlt").value=image[2];
document.getElementById("file_path").value=image[3];
document.getElementById("Imgalign").value=image[4];
document.getElementById("vspace").value=image[5]!="-1"?image[5]:"";
document.getElementById("hspace").value=image[6]!="-1"?image[6]:"";
document.getElementById("insertImg").value='<%=ResourceManager.GetString("mof")%>';
document.getElementById("previewImg").innerHTML='<img src='+image[3]+' align="middle" onload="w=this.width;h=this.height;if(this.width>300){this.width=300;this.height=Math.round(this.width*h/w);}if(this.height>225){this.height=225;this.width=Math.round(this.height*w/h);}" />';
}
}
</script>
</form>
</body>
</html>

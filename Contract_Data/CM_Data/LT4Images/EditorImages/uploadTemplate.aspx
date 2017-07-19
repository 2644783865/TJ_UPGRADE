<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Inherits="LongTrueComEditor.UpLoad"%>
<%@ Import Namespace="LongTrueComEditor" %>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<base target="_self" />
<script language="javascript" type="text/javascript">
var userAgent = navigator.userAgent.toLowerCase();
var is_ie = (userAgent.indexOf('msie') != -1);
function loading(showmessage)
{
document.getElementById("loading").style.visibility="visible";
document.getElementById("statusmessage").innerHTML=showmessage;
return true;
}
function insertTemplate()
{
if(document.getElementById("file_path").value!="")
{
if(is_ie)
{
window.returnValue = document.getElementById("templateContent").value;
}
else
{
window.opener.inserObject(null,'template',document.getElementById("templateContent").value);
}
}
window.close();
}
function add(name)
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
document.getElementById("file_path").focus();
}
var sTitle='<%=ResourceManager.GetString("uploadtemplate")%>';
window.focus();
document.write("<TITLE>" + sTitle + "</TITLE>");
</script>
<link href="stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0">
<form id="uploadFace" runat="server">
<table border="0" align=center style="word-break:break-all" width="100%">
<tr>
<td colspan="3" rowspan="3">
<fieldset><legend><span style="color: darkgray"><span style="color: gray"><%=ResourceManager.GetString("uploadface")%></span>&nbsp;
</span></legend>
<%=ResourceManager.GetString("uploadpath")%>：<asp:Label ID="path" runat="server" ForeColor="Black"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="returndir" OnClick="returndir_Click" ImageUrl="img/parentfolder.gif" runat=server/>..<br />
<%=ResourceManager.GetString("uploadfiles")%>：<asp:FileUpload ID="FileUpload1" runat="server" Width="388px" Height="21px" TabIndex="2" Font-Size="10pt" />
<asp:Button ID="uploadBtn" runat="server" OnClick="UploadBtn_Click"/><br />
<%=ResourceManager.GetString("filepath")%>：<asp:TextBox ID="file_path" runat="server" Width="316px" TabIndex="1"></asp:TextBox>
<asp:Button ID="insertTemplate" runat="server" OnClick="insertTemplate_Click" /><br />
[ <%=ResourceManager.GetString("uploaduse")%>：<asp:label ID="useSpace" ForeColor="Red" runat=server />，<%=ResourceManager.GetString("have")%>：<asp:label ID="space" ForeColor="Red" runat=server /><%=ResourceManager.GetString("singlesize")%>：<asp:Label ID="maxSingleUploadSize" runat="server" ForeColor="Red"></asp:Label>
]</fieldset>
<br /><fieldset style="text-align: center"><legend><span style="color: darkgray"><span style="color: gray"><%=ResourceManager.GetString("filelist")%></span>&nbsp;
</span></legend>
<div id="fileshow" style="border-right: 1.5pt inset; border-top: 1.5pt inset; vertical-align: middle;
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
<img src="img/filetype/<%#DataBinder.Eval(Container.DataItem,"Attributes").ToString().ToLower()=="directory"?"folder":((string)DataBinder.Eval(Container.DataItem,"Extension")).Replace(".","")%>.gif" /><asp:LinkButton ID="ListID" Text=<%#DataBinder.Eval(Container.DataItem,"Name")%> style="cursor:pointer; word-break:break-all" ForeColor="#000000" Font-Underline=false onmouseout="this.style.textDecoration='none'" onmouseover="this.style.textDecoration='underline'" CommandArgument=<%#DataBinder.Eval(Container.DataItem,"Name").ToString()%> OnCommand="SetServerCookie" OnClientClick=<%#DataBinder.Eval(Container.DataItem,"Attributes").ToString().ToLower()!="directory"?DataBinder.Eval(Container.DataItem,"Name","javascript:add(\"{0}\");return false;"):""%> runat="server"/>
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
</asp:GridView></div><table width="100%" border="0px"><tr><td valign="baseline" style="height: 23px">
[<%=ResourceManager.GetString("controlmenu")%>]：<asp:ImageButton id="selectAllBtn" runat="server" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" ImageUrl="img/selectall.gif" onclick="selectAllBtn_Click" />&nbsp;
<asp:ImageButton ID="deleteBtn" runat="server" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" ImageUrl="img/delete.gif" OnClick="deleteBtn_Click" />&nbsp;&nbsp;<asp:ImageButton id="editBtn" ImageUrl="img/rename.gif" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" runat="server" onclick="editBtn_Click" />&nbsp;&nbsp;<asp:ImageButton ID="newfolderBtn" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" runat="server" ImageUrl="img/newfolder.gif" OnClick="newfolderBtn_Click" />&nbsp;&nbsp;<asp:ImageButton ID="returndir2" OnClick="returndir_Click" ImageUrl="img/parentfolder.gif" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" runat=server/>&nbsp;&nbsp;<input language="javascript" onmouseup="if(is_ie){showModalDialog('find.aspx',this,'dialogWidth:320px;dialogHeight:130px;status:0;scroll:no');}else{window.find();}"
type=image src="img/search.gif" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" title='<%=ResourceManager.GetString("findfile")%>' />&nbsp;
<input language="javascript" onclick="window.close();" type="image" onMouseOver="this.className='overcolor'" onMouseOut="this.className=''" src="img/close.gif" title="<%=ResourceManager.GetString("close")%>" /></td>
<td align="right" style="height: 23px">
<img border=0px src="img/logo_S.png" /></td>
</tr></table>
</fieldset>
<asp:HiddenField ID="config_watermark" runat="server" />
<asp:HiddenField ID="config_watermarkText" runat="server" />
<asp:HiddenField ID="config_watermarkImages" runat="server" />
<asp:HiddenField ID="config_watermarkImages_path" runat="server" />
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
<asp:HiddenField ID="config_type" Value="Template"  runat="server" />
<asp:HiddenField ID="templateContent" runat="server" />
</td>
</tr>
</table>
<div id="loading" style="border-right: #333333 1px dashed; border-top: #333333 1px dashed;
font-size: 9pt; visibility: hidden; border-left: #333333 1px dashed;
width: 270px; color: #000000; border-bottom: #333333 1px dashed; position: absolute; height: 120px; background-color: #ffffff">
<center>
<br />
<br />
<span id="statusmessage"></span></center>
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
}
else
{
document.body.bgColor="#E0E0E0";
}
</script>
</form>
</body>
</html>

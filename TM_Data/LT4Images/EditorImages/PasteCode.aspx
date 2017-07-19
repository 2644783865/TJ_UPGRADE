<%@ Page Language="C#" AutoEventWireup="true" Inherits="LongTrueComEditor.CodeConverter" validateRequest="false" %>
<%@ Import Namespace="LongTrueComEditor" %>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html;charset=gb2312">
<title><%=ResourceManager.GetString("codehighlighter")%></title>
<base target="_self" />
<link href="stylesheet.css" rel="stylesheet" type="text/css" />
<script language=javascript>
var userAgent = navigator.userAgent.toLowerCase();
var is_ie = (userAgent.indexOf('msie') != -1);
function inserteditor()
{
var codstyle = "<div style='BORDER-RIGHT: #cccccc 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #cccccc 1px solid; PADDING-LEFT: 4px; FONT-SIZE: 13px; PADDING-BOTTOM: 4px; BORDER-LEFT: #cccccc 1px solid; WIDTH: 98%; WORD-BREAK: break-all; PADDING-TOP: 4px; BORDER-BOTTOM: #cccccc 1px solid; BACKGROUND-COLOR: #eeeeee'>";
if(is_ie)
{
window.parent.returnValue=codstyle+document.getElementById("htmlcontent").value+"</div>";
}
else
{
window.opener.plugin_execommand(codstyle+document.getElementById("htmlcontent").value+"</div>");
}
window.parent.close();
}
</script>
</head>
<body topmargin="10">
<form id="code" runat="server">
	<table border="0" cellpadding="0"cellspacing="0" width="100%">
	<tr>
		<td align="center">
			<div align="center">
				<table border=1 style="border-style:dashed ;"  bordercolor="#cccccc" >
					<tr>
						<td style="width: 86px; height: 32px" align="right">
                            <%=ResourceManager.GetString("codetype")%>：</td>
						<td style="width: 487px; height: 32px">
                            &nbsp;<asp:DropDownList ID="ddlLanguages" runat="server">
                            </asp:DropDownList>&nbsp;
                            <asp:CheckBox ID="chkIncludeLineNumbers" runat="server" />&nbsp;<asp:RadioButton
                                ID="rdoUsePreTag" runat="server" GroupName="whitespaceoptions"/>&nbsp;<asp:RadioButton ID="rdoConvertWhitespace" runat="server"
                                    GroupName="whitespaceoptions" Checked="True" /></td>
					</tr>
                    <tr>
                        <td align="right" style="width: 86px; height: 32px">
                            <%=ResourceManager.GetString("codestyle")%>：</td>
                        <td style="width: 487px">
                            &nbsp;<asp:DropDownList ID="ddlStyleType" runat="server">
                                <asp:ListItem Selected="True" Value="Inline Styles">Inline Styles</asp:ListItem>
                                <asp:ListItem Value="Inline Tags">Inline Tags</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
					<tr>
						<td style="width: 86px; height: 300px;" align="right">
                            <%=ResourceManager.GetString("codecontent")%>：<br />
                            [<%=ResourceManager.GetString("pastecode")%>]</td>
						<td align="center"><asp:TextBox Runat="server" ID="txtCodeInput" TextMode="MultiLine" Rows="10" Columns="80" Height="310px" Width="100%" BorderColor="Gray" BorderStyle="Dashed" BorderWidth="1px"/></td>
					</tr>
					<tr>
						<td style="height: 30px;" align="center" colspan="2">
                            <asp:Button ID="btnCodeHtmlify" runat="server"/>
                            <input type=Button ID="close" value="<%=ResourceManager.GetString("close2")%>"  OnClick="window.close()"/></td>
					</tr>
				</table>
			</div>
            <asp:HiddenField ID="htmlcontent" runat="server" />
		</td>
		<td></td>
	</tr>
	<tr>
		<td class="FooterBar" colspan="2" align="center"></TD>
	</tr>	
	</table>
</form>
</body>
<script language=javascript>
var userAgent = navigator.userAgent.toLowerCase();
var is_ie = (userAgent.indexOf('msie') != -1);
if(is_ie)
{
document.body.bgColor="ButtonFace";
}
else
{
document.body.bgColor="#E0E0E0";
}
</script>
</html>

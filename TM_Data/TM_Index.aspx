﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Index.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PMS项目管理系统-->技术管理</title>
</head>
<frameset id="mfrm" rows="70,*,5" cols="*" framespacing="0" frameborder="0" border="0"
    scrolling="yes">
    <%--<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
		<frame src="../top.aspx" name="topFrame" target="left" noresize="noresize" scrolling="no" id="topFrame" title="topFrame"  >
		<frameset rows="*" cols="150,*" framespacing="0" frameborder="0" border="0" id="frame" scrolling="yes">
			<frame src="TM_Menu.aspx" name='left' target="right"  scrolling="auto" noresize='noresize' />
            <frame src="TM_Desk.aspx" name='right' target="_self" scrolling="auto" noresize='noresize' />
		</frameset>
		<frame src="../bottom.aspx" name="bottomFrame" scrolling="No" noresize="noresize" id="bottomFrame" title="bottomFrame" title="bottomFrame" />
	</frameset>
</html>

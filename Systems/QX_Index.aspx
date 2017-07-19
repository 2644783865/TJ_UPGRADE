<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QX_Index.aspx.cs" Inherits="ZCZJ_DPF.Systems.QX_Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>中材重机数字化管理平台-->系统设置</title>
</head>
<frameset id="mfrm" id="mfrm" rows="93,*,32" cols="*" framespacing="0" frameborder="0"
    border="0" scrolling="yes">
		<frame src="../top.aspx" name="topFrame" target="left" noresize="noresize" scrolling="no" id="topFrame" title="topFrame"  >
		<frameset rows="*" cols="150,*" framespacing="0" frameborder="0" border="0" id="frame" scrolling="yes">
			<frame src="QX_Menu.aspx" name='left' target="right"  scrolling="auto" noresize='noresize' />
            <frame src="QX_Desk.aspx" name='right'  target="_self" scrolling="auto" noresize='noresize' />
		</frameset>
		<frame src="../bottom.aspx" name="bottomFrame" scrolling="No" noresize="noresize" id="bottomFrame" title="bottomFrame" title="bottomFrame" />
	</frameset>
</html>

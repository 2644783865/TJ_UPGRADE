<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_Index.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>人事行政管理</title>
</head>
  <frameset id="mfrm" rows="70,*,5" framespacing="0" frameborder="0" border="0">
		<frame src="../top.aspx" name="topFrame" noresize="noresize" scrolling="No" id="topFrame" title="topFrame" />
		<frameset cols="180,*" framespacing="0" frameborder="0" border="0" id="frame">
			<frame src="OM_MenuGai1.aspx" name='left' scrolling="auto" noresize="noresize" />
            <frame src="OM_Desk.aspx" name='right' scrolling="auto" noresize="noresize" />
		</frameset>
		<frame src="../bottom.aspx" name="bottomFrame" scrolling="No" noresize="noresize" id="bottomFrame" title="bottomFrame" />
  </frameset>
<body></body>
</html>

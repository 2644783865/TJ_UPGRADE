<%@ Page Language="C#" validateRequest="false"  AutoEventWireup="true" CodeBehind="CM_Notice.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Notice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="LongTrueComEditor" Namespace="LongTrueComEditor" TagPrefix="DNTB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�в��ػ����ֻ�����ƽ̨</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <script language="javascript" type="text/javascript">

        var displayBar=true;
        var displayTop=true;
        function switchBar(obj)
        {
	        if (displayBar)
	        {
		        parent.frame.cols="0,*";
		        displayBar=false;
		        obj.src="../Assets/images/bar_show.gif";
		        obj.title="����߹���˵�";
	        }
	        else{
		        parent.frame.cols="150,*";
		        displayBar=true;
		        obj.src="../Assets/images/bar_hide.gif";
		        obj.title="�ر���߹���˵�";
	        }
        }

        function switchTop(obj)
        {
	        var mfrm = parent.document.getElementById("mfrm"); 
	        if (mfrm == null)
	        {
		        alert("NULL");
		        return;
	        }
	        if (displayTop)
	        {
		        mfrm.rows="0,*,32";
		        displayTop=false;
		        obj.src="../Assets/images/bar_down.gif";
		        obj.title="��";
	        }
	        else{
		        mfrm.rows="93,*,32";
		        displayTop=true;
		        obj.src="../Assets/images/bar_up.gif";
		        obj.title="����";
	        }
        }
    </script>    		
</head>
<body>
 <form id="form1" runat="server">
       <div class="RightContentTop">
        <asp:Image ID="Image1" ImageUrl="~/Assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" />
        <div class="RightContentTitle">
            <table width="100%">
                <tr>
                    <td width="15">
                        <asp:Image ID="Image2" AlternateText="�ر���߹���˵�" style="CURSOR: hand" onClick="switchBar(this)"  CssClass="CloseImage" ImageUrl="~/Assets/images/bar_hide.gif" runat="server" /></td>
                    <td>������ϵ������</td>
                    <td width="15">
                        <asp:Image ID="Image3" ImageUrl="~/Assets/images/bar_up.gif" AlternateText="����" style="CURSOR: hand" onClick="switchTop(this)" runat="server" /></td>
                </tr>
            </table>
       </div> 
       </div> 
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
           </asp:ToolkitScriptManager>
      <%-- <div class="box-inner">--%>
       <div class="box_right">
       <div class='box-title'>
          
       <table>
        <tr>           
                <td align="left" width="20%">
                <asp:Button ID="btnsubmit" runat="server" Text="ȷ ��" onclick="btnsubmit_Click" />
                 &nbsp;&nbsp;
                <asp:Button ID="btnreturn" runat="server" Text="�� ��" onclick="btnreturn_Click" 
                     />                
            </td>
        </tr>
       </table>
       </div>
       </div>
    <%--   </div>--%>
       
      <%-- <div class="box-wrapper">--%>
        <div class="box-outer">
         <table width='100%' align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Panel ID="panel3" runat="server" >
        <tr align="center">
            <td style=" font-size:large; text-align:center;" colspan="4">��ϵ��</td>
        </tr>
        <tr align="center">
        <td  align="right" >��Ŀ��ţ�</td>
        <td  align="left" colspan="3" >
            <asp:ComboBox ID="ComboBox1" runat="server" Width="100px" AutoPostBack="true" 
                onselectedindexchanged="ComboBox1_SelectedIndexChanged">
            </asp:ComboBox>&nbsp;&nbsp;
            <asp:TextBox ID="tbprojectID" runat="server" Visible="false"></asp:TextBox>
            ��Ŀ���ƣ� <asp:TextBox ID="tbproject" runat="server" Enabled="false" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             ��ϵ����ţ�<asp:TextBox ID="CM_tbID" runat="server" Enabled="false"></asp:TextBox>
        </td>
        
       </tr>
       <tr>
       <td align="right">��&nbsp;&nbsp;&nbsp;&nbsp;�ԣ�</td>
       <td><asp:TextBox ID="tbOriginate" runat="server" Enabled="false"></asp:TextBox></td>
        <td align="right">�·����ţ�</td>
        <td align="left">
                <asp:CheckBoxList ID="CB_Department" runat="server" RepeatDirection="Horizontal">
               <%-- <asp:ListItem Text="�쵼" Value="0"></asp:ListItem>
                <asp:ListItem Text="������" Value="1"></asp:ListItem>
                <asp:ListItem Text="������" Value="2"></asp:ListItem>
                 <asp:ListItem Text="������" Value="3"></asp:ListItem>
                <asp:ListItem Text="�ɹ���" Value="4"></asp:ListItem>
                <asp:ListItem Text="���˲�" Value="5"></asp:ListItem>
                <asp:ListItem Text="�������첿" Value="6"></asp:ListItem>--%>
                </asp:CheckBoxList>
       </td>
       </tr>
      
        <tr align="center">
       <td  align="right">�������</td>
        <td  align="left" colspan="3" >
          <asp:RadioButtonList ID="rdtype" runat="server" RepeatDirection="Horizontal" >
               <asp:ListItem Text="��ͬ��Ϣ" Value="1"></asp:ListItem>
                 <asp:ListItem Text="������Ϣ" Value="2"></asp:ListItem>
                   <asp:ListItem Text="������Ϣ" Value="3"></asp:ListItem>
            </asp:RadioButtonList>
           
        </td>
       
       </tr>
       <tr align="center">
       <td  align="right">&nbsp;&nbsp;�����ˣ�</td>
        <td  align="left"><asp:TextBox ID="tbEditor" runat="server" Enabled="false"></asp:TextBox></td>
        <td>�������ڣ�</td>
         <td align="left"><asp:TextBox ID="tbDate" runat="server" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr align="center" >
        <td align="right" >����</td>
           <td align="left" colspan="3" >
            <dntb:webeditor id="txtNews_content" runat="server" Width="90%" 
            AutoSaveImgToLocal="true"  getImagesPathID="txtNews_UploadImage" 
            UploadFolder="../../UploadFiles/News/" SideMenuWidth="0px" 
            UploadConfig="Upload500MB.config"></dntb:webeditor><br />
           <asp:Label ID="tbcontent" Visible="false" runat="server"  Height="200px" Width="90%"></asp:Label>
           </td>           
        </tr>  
        </asp:Panel>    
        </table>
    </div>
  <%--  </div>--%>
      </form>
</body>
</html>

      



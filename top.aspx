<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="top.aspx.cs" Inherits="ZCZJ_DPF.top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="Assets/main.css" rel="stylesheet" type="text/css" />

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript">
    document.onkeypress=check; 
    document.onkeydown=check; 
    function SelectMenu(id)
    {
       var a=id;
       document.getElementById("<%=gdid.ClientID%>").value=id;
            
       __doPostBack('<%=gdid.ClientID %>',"");
    }
    
    window.onload=function aa()
    {
    
    if(document.getElementById("<%=gdid.ClientID%>").value!=""&&document.getElementById("<%=gdid.ClientID%>").value!=null)
    {
      document.getElementById(document.getElementById("<%=gdid.ClientID%>").value).style.color="red";
     }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table id="top" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td rowspan="2" width="170">
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Image ID="Image2" ImageUrl="~/Assets/images/zc_logo.gif" runat="server" 
                    Height="45px" Width="275px" />
            </td>
            <td align="center" valign="bottom" style="font-size:x-large; font-weight:bold">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�в�(���)���ͻ�е���޹�˾���ֻ�����ƽ̨</td>
            <td align="right">
                <div class="loginInfo">
                    ��ǰ�û���<asp:Label ID="labAdmin" runat="server" Text="Label"></asp:Label>
                    | <span>���ţ�<asp:Label ID="labDepart" runat="server" Text=""></asp:Label></span>
                    |
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="Red">��ȫ�˳�</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;</div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <span class="top_des">
                    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                    <asp:HyperLink ID="HyperLink8" NavigateUrl="~/MT_Data/Mt_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink8')" runat="server">��������|</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink11" NavigateUrl="~/OM_Data/OM_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink11')" runat="server">������������|</asp:HyperLink>
                    <asp:HyperLink ID="HypProMang" NavigateUrl="~/CM_Data/CM_Index.aspx" Target="_top"
                        onclick="SelectMenu('HypProMang')" runat="server">�г�����|</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/TM_Data/TM_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink1')" runat="server">��������|</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink2" NavigateUrl="~/PC_Data/PC_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink2')" runat="server">�ɹ�����|</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink3" NavigateUrl="~/PM_Data/PM_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink3')" runat="server">��������|</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink7" NavigateUrl="~/ESM_Data/EQU_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink7')" runat="server">�豸��ȫ|</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink4" NavigateUrl="~/QC_Data/QC_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink4')" runat="server">��������|</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink5" NavigateUrl="~/SM_Data/SM_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink5')" runat="server" Visible="false">�ֿ����|</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink6" NavigateUrl="~/FM_Data/FM_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink6')" runat="server">�������|</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink9" NavigateUrl="Contract_Data/Contract_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink9')" runat="server">��ͬ����|</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink10" NavigateUrl="PL_Data/PL_Index.aspx" Target="_top"
                        onclick="SelectMenu('HyperLink10')" runat="server">��ϵ����|</asp:HyperLink>
                    <asp:HyperLink ID="HypBasicData" NavigateUrl="Basic_Data/BD_Index.aspx" Target="_top"
                        onclick="SelectMenu('HypBasicData')" runat="server">��������</asp:HyperLink>

                    <div style="display: none">
                        <asp:TextBox ID="gdid" runat="server" AutoPostBack="true" OnTextChanged="gdid_TextChanged"></asp:TextBox>
                    </div>
                </span>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

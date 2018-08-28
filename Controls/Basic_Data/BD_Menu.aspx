<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BD_Menu.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.BD_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
     <script src="../JS/EasyUI/jquery.min.js" type="text/javascript"></script>
     <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
     <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

</head>
<body id="leftMenu"class="easyui-layout" >
    <form id="form1" runat="server">

    <script language="javascript" type="text/javascript">
    function SelectMenu(num)
    {
    for(var i=1;i<=18;i++)
    {
        if( document.getElementById("HyperLink"+i)!=null)
        document.getElementById("HyperLink"+i).className='LeftMenuNoSelected';
    }
        if( document.getElementById("HyperLink"+num)!=null)
        document.getElementById("HyperLink"+num).className='LeftMenuSelected';
    }
    </script>
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" Interval="60000">    </asp:Timer>
      <div  region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;����ѡ��"  data-options="collapsible:false"  >


   <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div id="menuContent" class="easyui-accordion"  fit="true"` >
            <div title="�������ݹ���" style="overflow:auto;background-color: #E3F1FA;" >
            <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>���Ź���</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>������Ϣ����</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>���Ϸ������</p></asp:HyperLink> 
             <asp:HyperLink ID="HyperLink13" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>�б����Ϲ���</p></asp:HyperLink>    
                  
                         <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>�������Ϲ���</p></asp:HyperLink>   
                  
                
             <asp:HyperLink ID="HyperLink8" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>��������</p></asp:HyperLink> 
            <asp:HyperLink ID="HyperLink7" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>������Ϣ����</p></asp:HyperLink>
                
             <asp:HyperLink ID="HyperLink14" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                     <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                    </Triggers>
                    <ContentTemplate>
                    <p>��������<asp:Label ID="CUSUP_REVIEW" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                    </asp:UpdatePanel>                 
                </asp:HyperLink>
            <asp:HyperLink ID="HyperLink11" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>Ȩ������</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>Ȩ�޹���</p></asp:HyperLink>
                 <asp:HyperLink ID="HyperLink9" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server" NavigateUrl="~/Basic_Data/QX_View_List.aspx"><p>�鿴Ȩ������</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>�޸�����</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink10" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>�����޸ļ�¼</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink12" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server" NavigateUrl="~/OM_Data/OM_GZ_JL.aspx"><p>���ʲ�ѯ��¼</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink15" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server" NavigateUrl="~/Basic_Data/TM_GsBaseManagement.aspx"><p>��ʱ��������</p></asp:HyperLink>
            
            
            <asp:HyperLink ID="HyperLink16" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server" NavigateUrl="~/Basic_Data/TM_JICHUANGBASE.aspx"><p>������Ϣ����</p></asp:HyperLink>
          
        </div>
       </div>
    </div>
    </form>
</body>
</html>

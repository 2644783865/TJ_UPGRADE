<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PL_Menu.aspx.cs" Inherits="ZCZJ_DPF.PL_Data.PL_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
        <script src="../JS/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
</head>
<body id="leftMenu"class="easyui-layout" >
    <form id="form1" runat="server">

    <script language="javascript" type="text/javascript">
        function SelectMenu(num) {
            for (var i = 1; i <= 9; i++) {
                if (document.getElementById("HyperLink" + i)) {
                    document.getElementById("HyperLink" + i).className = 'LeftMenuNoSelected';
                }
            }
            if (document.getElementById("HyperLink" + num)) {
                document.getElementById("HyperLink" + num).className = 'LeftMenuSelected';
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" Interval="30000" runat="server">
    </asp:Timer>
<div  region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;����ѡ��"  data-options="collapsible:false"  >
   
      <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
  <div id="menuContent" class="easyui-accordion"  fit="true"` >
  <div title="���ȹ���" style="overflow:auto;background-color: #E3F1FA;" >
     <%--       <asp:HyperLink ID="HyperLink5" runat="server" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                Target="right">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            ���Ȳ鿴<asp:Label ID="lblPlan" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            

            
           <asp:HyperLink ID="HyperLink2" CssClass="LeftMenuNoSelected" NavigateUrl="~/PL_Data/MainPlanModel_View.aspx"
                Target="right" runat="server"><p>���ƻ�ģ�����</p></asp:HyperLink>
                  <asp:HyperLink ID="HyperLink1" CssClass="LeftMenuNoSelected" NavigateUrl="~/PL_Data/MainPlanProView.aspx"
                Target="right" runat="server"><p>�����ƻ�����</p></asp:HyperLink>--%>
                
                
           <%--      <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>��Ŀ���</p></asp:HyperLink> 
             <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>���ȱ༭</p></asp:HyperLink>  
             <asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>�����ƻ�</p></asp:HyperLink>    --%>
                
                  <asp:HyperLink ID="HyperLink1" runat="server" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                Target="right" NavigateUrl="~/QC_Data/QC_TargetAnalyze_List.aspx">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            Ŀ��ֽ�<asp:Label ID="lblTargetAnalyze" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
              
                  <asp:HyperLink ID="HyperLink2" runat="server" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                Target="right" NavigateUrl="~/QC_Data/QC_Management_Audit.aspx">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            ��������<asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
                   <asp:HyperLink ID="HyperLink3" runat="server" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                Target="right" NavigateUrl="~/QC_Data/QC_Internal_Audit.aspx?type=inner">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            �ڲ����<asp:Label ID="lblInnerAudit" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
                   <asp:HyperLink ID="HyperLink4" runat="server" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                Target="right" NavigateUrl="~/QC_Data/QC_Internal_Audit.aspx?type=group">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            ��������<asp:Label ID="lblGroupAudit" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
                   <asp:HyperLink ID="HyperLink5" runat="server" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                Target="right" NavigateUrl="~/QC_Data/QC_External_Audit.aspx">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            �ⲿ���<asp:Label ID="lblExterAudit" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
        </div>
    </div>
   </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EQU_Menu.aspx.cs" Inherits="ZCZJ_DPF.ESM.EQU_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    function SelectMenu(num)
    {
        for(var i=1;i<15;i++)
        {
            if(document.getElementById("HyperLink"+i)!=null)
            document.getElementById("HyperLink"+i).className='LeftMenuNoSelected';
        }
        if(document.getElementById("HyperLink"+num)!=null)
        document.getElementById("HyperLink"+num).className='LeftMenuSelected';
    }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" runat="server" Interval="60000">
    </asp:Timer>
    <div  region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;功能选项"  data-options="collapsible:false"  >
       <div id="menuContent" class="easyui-accordion"  fit="true"` >
        <div title="设备安全管理" style="overflow:auto;background-color: #E3F1FA;" >
            <asp:HyperLink ID="HyperLink1" runat="server" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                NavigateUrl="EQU_List.aspx" Target="right"><p>设备管理</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink2" runat="server" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                NavigateUrl="EQU_tzsb.aspx" Target="right"><p>特种设备管理</p></asp:HyperLink>
            <%--<asp:HyperLink ID="HyperLink3" runat="server" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                NavigateUrl="EQU_Need_List.aspx" Target="right"><p>设备需用计划</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink4" runat="server" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                NavigateUrl="EQU_Need_Audit.aspx" Target="right">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            设备采购审批<asp:Label ID="lbl_cgsp" runat="server" ForeColor="Red"></asp:Label>
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>--%>
           <%-- <asp:HyperLink ID="HyperLink5" runat="server" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                NavigateUrl="EQU_Part_Store.aspx" Target="right"><p>备件库存</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink6" runat="server" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                NavigateUrl="EQU_Part_In.aspx" Target="right"><p>备件入库管理</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink7" runat="server" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                NavigateUrl="EQU_Part_Out.aspx" Target="right"><p>备件出库管理</p></asp:HyperLink>--%>
                
            <asp:HyperLink ID="HyperLink5" runat="server" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                NavigateUrl="~/SM_Data/SM_Warehouse_Query.aspx?FLAG=QUERY" Target="right"><p>库存查询</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink6" runat="server" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                NavigateUrl="~/SM_Data/SM_WarehouseIN_Index.aspx" Target="right"><p>入库管理</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink7" runat="server" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                NavigateUrl="~/SM_Data/SM_WarehouseOut_Index.aspx" Target="right"><p>出库管理</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink8" runat="server" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                NavigateUrl="EQU_Repair_List.aspx" Target="right"><p>维修计划</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink9" runat="server" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                NavigateUrl="EQU_Repair_Audit.aspx" Target="right">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            维修申请审批<asp:Label ID="lbl_wxsp" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink10" runat="server" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                NavigateUrl="~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx" Target="right"><p>新增采购申请</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink11" runat="server" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                NavigateUrl="~/PC_Data/PC_TBPC_Otherpur_Bill_Audit.aspx" Target="right">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            采购申请审批<asp:Label ID="lbl_csp" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink12" runat="server" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                NavigateUrl="EQU_GXHT_GL.aspx" Target="right">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            设备合同管理<asp:Label ID="lbl_ssp" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink> 
        </div>
    </div>
    </div>
    </form>
</body>
</html>

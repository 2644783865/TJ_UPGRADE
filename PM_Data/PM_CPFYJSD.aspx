<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PM_CPFYJSD.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_CPFYJSD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    成品发运均摊
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
  function  CalWeightJe(){
    var tab=document.getElementById("tab");
    var a=0;
    for (i = 0; i < (tab.rows.length-1); i++)
    {
      $JE= $("#tab input[name*=JS_HSJE]")
      var je=$JE.val();
      if(je==0)
      {
        a++;
      }
    }
    if(a>0)
    {
        return confirm('是否确定提交?有发运金额为0');
    }
    
    }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right" width="100%">
                                <asp:Button runat="server" ID="btnSubmit" Text="提交" OnClick="btnSubmit_OnClick" Width="40px" OnClientClick="return CalWeightJe()"
                                    Height="30px" />
                                    
                                <asp:Button runat="server" ID="btnDelete" Text="删除" OnClick="btnDelete_OnClick" Width="40px"
                                    Height="30px" />
                                <asp:Button runat="server" ID="btnBack" Text="返回" OnClick="btnBack_OnClick" Width="40px"
                                    Height="30px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div>
        <table width="100%">
            <tr>
                <th style="width: 20%;">
                    &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;号：<asp:Label ID="lbJS_BH" runat="server"></asp:Label>
                </th>
                <th style="width: 20%;">
                    &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:Label ID="lbJS_RQ" runat="server"></asp:Label>
                </th>
                <th style="width: 20%;">
                    &nbsp;&nbsp;&nbsp;制单人：&nbsp;&nbsp;&nbsp;<asp:Label ID="lbJS_ZDR" runat="server"></asp:Label>
                </th>
                <th style="width: 40%;">
                    &nbsp;&nbsp;&nbsp;供应商：<asp:Label ID="lbJS_GYS" runat="server"></asp:Label>
                </th>
            </tr>
            <tr>
                <th colspan="4">
                    &nbsp;&nbsp;&nbsp;备注：
                    <asp:TextBox ID="txtJS_BZ" runat="server" TextMode="MultiLine" Rows="3" Width="80%"></asp:TextBox>
                </th>
            </tr>
        </table>
        <table id="tab" class="nowrap cptable fullwidth" align="center">
            <asp:Repeater runat="server" ID="rptJSD">
                <HeaderTemplate>
                    <tr align="center" class="tableTitle" style="background-color: #48D1CC; height: 30px">
                        <th>
                            <strong>序号</strong>
                        </th>
                        <th>
                            <strong>计划跟踪号</strong>
                        </th>
                        <th>
                            <strong>合同号</strong>
                        </th>
                        <%-- <th>
                            <strong>项目名称</strong>
                        </th>--%>
                        <th>
                            <strong>任务号</strong>
                        </th>
                        <th>
                            <strong>总序</strong>
                        </th>
                        <%--<th>
                            <strong>设备名称</strong>
                        </th>--%>
                        <%--<th>
                            <strong>图号</strong>
                        </th>--%>
                        <th>
                            <strong>发货商</strong>
                        </th>
                        <th>
                            <strong>交货期</strong>
                        </th>
                        <th>
                            <strong>收货单位</strong>
                        </th>
                        <th>
                            <strong>发货数量</strong>
                        </th>
                        <%--                        <th>
                            <strong>结算数量</strong>
                        </th>--%>
                        <th>
                            <strong>单重</strong>
                        </th>
                        <%--                        <th>
                            <strong>结算总重</strong>
                        </th>--%>
                        <th>
                            <strong>税率</strong>
                        </th>
                        <th>
                            <strong>金额（含税）</strong>
                        </th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                        <td>
                            <asp:CheckBox runat="server" ID="cbxXUHAO" Visible="false"/>
                            <asp:Label runat="server" ID="XUHAO" Text='<%#Container.ItemIndex + 1%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="JS_JHGZH" Text='<%#Eval("JS_JHGZH")%>'></asp:Label><%--计划跟踪号--%>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="JS_HTH" Text='<%#Eval("JS_HTH")%>'></asp:Label><%--合同号--%>
                        </td>
                        <%--<td>
                            <asp:Label runat="server" ID="JS_XMMC" Text='<%#Eval("JS_XMMC")%>'></asp:Label>
                        </td>--%><%--项目名称--%>
                        <td>
                            <asp:Label runat="server" ID="JS_RWH" Text='<%#Eval("JS_RWH")%>'></asp:Label><%--任务号--%>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="JS_ZX" Text='<%#Eval("JS_ZX")%>'></asp:Label><%--总序--%>
                        </td>
                        <%--<td>
                            <asp:Label runat="server" ID="JS_SBMC" Text='<%#Eval("JS_SBMC")%>'></asp:Label>
                        </td>--%><%--设备名称--%>
                        <%--<td>
                            <asp:Label runat="server" ID="JS_TUHAO" Text='<%#Eval("JS_TUHAO")%>'></asp:Label>
                        </td>--%><%--图号--%>
                        <td>
                            <asp:Label runat="server" ID="JS_GYS" Text='<%#Eval("JS_GYS")%>'></asp:Label><%--供应商--%>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="JS_JHQ" Text='<%#Eval("JS_JHQ")%>'></asp:Label><%--交货期--%>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="JS_SHDW" Text='<%#Eval("JS_SHDW")%>'></asp:Label><%--收货单位 --%>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="JS_BJSL" Text='<%#Eval("JS_BJSL")%>'></asp:Label><%--发货数量--%>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="JS_DANZ" Text='<%#Eval("JS_DANZ")%>'></asp:Label><%--单重--%>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="JS_SHUIL" Text='<%#Eval("JS_SHUIL")%>'></asp:Label><%--税率--%>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="JS_HSJE" Text='<%#Eval("JS_HSJE")%>' name="JS_HSJE"></asp:TextBox><%--金额（含税）--%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
            没有数据！</asp:Panel>
    </div>
</asp:Content>

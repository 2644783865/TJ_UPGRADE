<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_SHCLDGL.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_SHCLDGL" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    售后质量问题处理单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        ul
        {
            list-style: none;
        }
        li
        {
            float: left;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="FixTable.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function() {
            $("#fg").hide();
            $("#bb").hide();
            $("#cc").hide();
            $("#dd").hide();
            $("#ee").hide();
        })

        function btnFG_onclick() {
            $("#fg").show();
            $("#fg").dialog({
                title: '指定“原因分析”填写人员',
                width: 220,
                height: 120,
                closed: false,
                cache: false,
                buttons: '#bb',
                modal: true
            });
            $("input[name='dept']").val("12");
        }

        function btnFG1_onclick() {
            $("#fg").show();
            $("#fg").dialog({
                title: '指定“处理意见”填写人员',
                width: 220,
                height: 120,
                closed: false,
                cache: false,
                buttons: '#cc',
                modal: true
            });
            $("input[name='dept']").val("03");
        }

        function btnFG2_onclick() {
            $("#fg").show();
            $("#fg").dialog({
                title: '指定“处理方案”填写人员',
                width: 220,
                height: 120,
                closed: false,
                cache: false,
                buttons: '#dd',
                modal: true
            });
            $("input[name='dept']").val("07");
        }

        function btnFG3_onclick() {
            $("#fg").show();
            $("#fg").dialog({
                title: '指定“费用统计”填写人员',
                width: 220,
                height: 120,
                closed: false,
                cache: false,
                buttons: '#ee',
                modal: true
            });
            $("input[name='dept']").val("06");
        }

        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txtTXR.ClientID %>").val(r.st_name);
                $("#<%=hidTXR.ClientID %>").val(r.st_name);
                $("#<%=hidTXRID.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }

        function btnReset_onclick() {
            $("#tabsx input:text").val('');
        }

        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[name*=CLD_ID]").val();
                console.info(id);
                window.open("CM_SHCLD.aspx?action=read&id=" + id);
            });
            
             $("#tab tr").click(function() {
                //   console.log($(this).attr("class"));
                $(this).removeAttr("style");
                $(this).toggleClass("techBackColor");
            });
        });

    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box_right">
            <div class="box-title" align="right">
                <table>
                    <tr>
                        <td>
                            <a id="btnDaoChu" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                                onserverclick="btnDaoChu_onclick">导出</a>
                        </td>
                        <td>
                            <a id="btnFG" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                                onclick="btnFG_onclick()">确定“原因分析”填写人</a>
                        </td>
                        <td>
                            <a id="btnFG1" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                                onclick="btnFG1_onclick()">确定“处理意见”填写人</a>
                        </td>
                        <td>
                            <a id="btnFG2" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                                onclick="btnFG2_onclick()">确定“处理方案”填写人</a>
                        </td>
                        <td>
                            <a id="btnFG3" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                                onclick="btnFG3_onclick()">确定“费用统计”填写人</a>
                        </td>
                        <td>
                            <a runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'"
                                id="btnDelete" onserverclick="btnDelete_OnClick">删除</a>
                        </td>
                        <td>
                            <a runat="server" id="btnAlter" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-edit'"
                                onserverclick="btnAlter_OnClick">修改</a>
                        </td>
                        <td>
                            <a runat="server" id="btnRefuse" href="#" visible="false" class="easyui-linkbutton" data-options="iconCls:'icon-edit'"
                                onserverclick="btnRefuse_OnClick">驳回</a>
                        </td>
                        <td>
                            <a runat="server" id="btnAdd" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                                onserverclick="btnAdd_OnClick">新增质量问题处理单</a>
                            <input type="hidden" runat="server" id="hidTXR" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblRW" AutoPostBack="true" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                                <asp:ListItem Text="我的任务" Value="2" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="cbxDFG" Text="待分工" AutoPostBack="true" OnCheckedChanged="Query" />
                        </td>
                        <td>
                            按审批状态：
                            <asp:DropDownList runat="server" ID="ddlSPZT" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                                <asp:ListItem Text="未审批" Value="2"></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="3"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="4"></asp:ListItem>
                                <asp:ListItem Text="未通过" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            按处理状态：
                            <asp:DropDownList runat="server" ID="ddlCLZT" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                                <asp:ListItem Text="未处理" Value="2"></asp:ListItem>
                                <asp:ListItem Text="处理中" Value="3"></asp:ListItem>
                                <asp:ListItem Text="已处理" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            按是否填写：
                            <asp:DropDownList runat="server" ID="ddlTXLX" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="原因分析" Value="CLD_YYFX"></asp:ListItem>
                                <asp:ListItem Text="处理意见" Value="CLD_CLYJ"></asp:ListItem>
                                <asp:ListItem Text="处理方案" Value="CLD_CLFA"></asp:ListItem>
                                <asp:ListItem Text="处理结果" Value="CLD_CLJG"></asp:ListItem>
                                <asp:ListItem Text="费用统计" Value="CLD_FWZFY"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList runat="server" ID="ddlTX" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                                <asp:ListItem Text="未填写" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已填写" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnQTSX" Text="其他筛选" BackColor="#00ffcc" />
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnQTSX"
                                PopupControlID="PanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                CancelControlID="btnClose" Y="80">
                            </asp:ModalPopupExtender>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:Panel ID="PanelCondition" runat="server" Width="75%" Style="display: none">
        <table id="tabsx" width="95%" style="background-color: #CCCCFF; border: solid 1px black;">
            <tr>
                <td colspan="5" align="center">
                    <a runat="server" id="btnQuery" onserverclick="Query" href="#" class="easyui-linkbutton"
                        data-options="iconCls:'icon-search'">查询</a> &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    <a id="btnReset" onclick="btnReset_onclick()" href="#" class="easyui-linkbutton"
                        data-options="iconCls:'icon-remove'">重置</a> &nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <input type="button" id="btnClose" value="关闭" />
                </td>
            </tr>
            <tr>
                <td>
                    编号
                </td>
                <td>
                    <input type="text" runat="server" id="txtBH" />
                </td>
                <td>
                    合同号
                </td>
                <td>
                    <input type="text" runat="server" id="txtHTH" />
                </td>
                <td>
                    项目名称
                </td>
                <td>
                    <input type="text" runat="server" id="txtXMMC" />
                </td>
            </tr>
            <tr>
                <td>
                    顾客名称
                </td>
                <td>
                    <input type="text" runat="server" id="txtGKMC" />
                </td>
                <td>
                    任务号
                </td>
                <td>
                    <input type="text" runat="server" id="txtRWH" />
                </td>
                <td>
                    设备名称
                </td>
                <td>
                    <input type="text" runat="server" id="txtSBMC" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div id="fg">
        <ul>
            <li>
                <asp:TextBox runat="server" ID="txtTXR" onfocus="this.blur()" Width="120px"></asp:TextBox>
                <input type="hidden" runat="server" id="hidTXRID" />
                <asp:Image runat="server" ID="imgTXR" ImageUrl="../Assets/images/username_bg.gif"
                    onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
            </li>
        </ul>
    </div>
    <div id="bb">
        <a id="btnYYFX" href="#" runat="server" class="easyui-linkbutton" onserverclick="btnYYFX_OnClick">
            确定</a>
    </div>
    <div id="cc">
        <a id="btnCLYJ" href="#" runat="server" class="easyui-linkbutton" onserverclick="btnCLYJ_OnClick">
            确定</a>
    </div>
    <div id="dd">
        <a id="btnCLFG" href="#" runat="server" class="easyui-linkbutton" onserverclick="btnCLFA_OnClick">
            确定</a>
    </div>
    <div id="ee">
        <a id="btnFYTJ" href="#" runat="server" class="easyui-linkbutton" onserverclick="btnFYTJ_OnClick">
            确定</a>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: 475px; overflow: auto; width: 100%">
                <div class="cpbox xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater runat="server" ID="rptSHFWCLD" OnItemDataBound="rptSHFWCLD_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <th>
                                        <strong>序号</strong>
                                    </th>
                                    <th>
                                        <strong>顾客名称</strong>
                                    </th>
                                    <th>
                                        <strong>编号</strong>
                                    </th>
                                    <th>
                                        <strong>合同号</strong>
                                    </th>
                                    <th>
                                        <strong>任务号</strong>
                                    </th>
                                    <th>
                                        <strong>项目名称</strong>
                                    </th>
                                    <th>
                                        <strong>设备名称</strong>
                                    </th>
                                    <th>
                                        <strong>信息简介</strong>
                                    </th>
                                    <th>
                                        <strong>处理结果</strong>
                                    </th>
                                    <th>
                                        <strong>服务总费用</strong>
                                    </th>
                                    <th>
                                        <strong>制单人</strong>
                                    </th>
                                    <th>
                                        <strong>制单日期</strong>
                                    </th>
                                    <th>
                                        <strong>负责部门</strong>
                                    </th>
                                    <th>
                                        <strong>审批状态</strong>
                                    </th>
                                    <th>
                                        <strong>处理状态</strong>
                                    </th>
                                    <th>
                                        <strong>审批</strong>
                                    </th>
                                    <th>
                                        <strong>填写原因分析</strong>
                                    </th>
                                    <th>
                                        <strong>填写处理意见</strong>
                                    </th>
                                    <th>
                                        <strong>填写处理方案</strong>
                                    </th>
                                    <th>
                                        <strong>填写处理过程及结果</strong>
                                    </th>
                                    <th>
                                        <strong>统计服务费用</strong>
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息">
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" />
                                        <asp:Label runat="server" ID="XuHao" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                        <input type="hidden" runat="server" id="CLD_ID" value='<%#Eval("CLD_ID") %>' name="CLD_ID" />
                                        <input type="hidden" runat="server" id="CLD_SPZT" value='<%#Eval("CLD_SPZT")%>' />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_GKMC" Text='<%#Eval("CLD_GKMC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_BH" Text='<%#Eval("CLD_BH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_HTH" Text='<%#Eval("CLD_HTH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_RWH" Text='<%#Eval("CLD_RWH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_XMMC" Text='<%#Eval("CLD_XMMC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_SBMC" Text='<%#Eval("CLD_SBMC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="CLD_XXJJ" Text='<%#Eval("CLD_XXJJ")%>' ToolTip='<%#Eval("CLD_XXJJ")%>'
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="CLD_FWGC" Text='<%#Eval("CLD_CLJG") %>' ToolTip='<%#Eval("CLD_CLJG") %>'
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_FWZFY" Text='<%#Eval("CLD_FWZFY")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_ZDR" Text='<%#Eval("CLD_ZDR")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_ZDSJ" Text='<%#Eval("CLD_ZDSJ")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_FZBM" Text='<%#Eval("CLD_FZBM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_SPZT1" Width="100%" Height="100%" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_CLZT" Width="100%" Height="100%" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplSP" NavigateUrl='<%#"CM_SHCLD.aspx?action=check&id="+Eval("CLD_ID")%>'>
                                            <asp:Image runat="server" ID="imgCheck" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/images/shenhe.gif" />
                                            审批
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplTXYY" NavigateUrl='<%#"CM_SHCLD.aspx?action=writeyy&id="+Eval("CLD_ID")%>'>
                                            <asp:Image runat="server" ID="Image3" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            填写
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplTXYJ" NavigateUrl='<%#"CM_SHCLD.aspx?action=writeyj&id="+Eval("CLD_ID")%>'>
                                            <asp:Image runat="server" ID="Image4" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            填写
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplTXFA" NavigateUrl='<%#"CM_SHCLD.aspx?action=writefa&id="+Eval("CLD_ID")%>'>
                                            <asp:Image runat="server" ID="Image1" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            填写
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplTXJG" NavigateUrl='<%#"CM_SHCLD.aspx?action=writejg&id="+Eval("CLD_ID")%>'>
                                            <asp:Image runat="server" ID="Image5" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            填写
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplTJ" NavigateUrl='<%#"CM_SHCLD.aspx?action=count&id="+Eval("CLD_ID")%>'>
                                            <asp:Image runat="server" ID="Image2" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            统计
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    <div id="win" visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        <a href="../PC_Data/PC_TBPC_Purchaseplan_assign_list.aspx"></a><strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        按部门查询：
                    </td>
                    <td>
                        <input id="dep" name="dept" value="" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 430px; height: 230px">
            <table id="dg">
            </table>
        </div>
    </div>
    <div id="buttons" style="text-align: right" visible="false">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
            保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>

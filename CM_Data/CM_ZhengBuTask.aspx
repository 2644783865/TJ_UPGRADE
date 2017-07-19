﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_ZhengBuTask.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_ZhengBuTask" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    经营计划单增补
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tb
        {
            width: 400px;
        }
        .center
        {
            text-align: center;
        }
    </style>

    <script type="text/javascript">

        function tiX(obj) {
            if (obj == "1") {
                alert('任务号里请勿输入汉字！');
            } else if (obj == "2") {
                alert('该项目不存在此任务号！');
            } else if (obj == "3") {
                alert('请输入正确的任务号（如14000SF1-1等）！');
            }
        }
        function ProAdd() {
            var tsa_id = document.getElementById('<%=TSA_ID.ClientID%>').value;
            if (tsa_id == "") {
                alert("请添加任务号！");
                event.returnValue = false;
                return false;
            }
            var pattem = /^\d{5}([A-Za-z0-9-])+/;
            if (!pattem.test(tsa_id)) {
                alert('请输入正确的任务号（如14000SF1-1等）！');
                return false;
            }
            if (/[\u2E80-\u9FFF]+/g.test(tsa_id)) {
                alert("任务号里请勿输入汉字！");
                return false;
            }
        }

        function checkId(obj) {
            var tsa = obj.value;
            var pattem = /^\d{5}([A-Za-z0-9-])+/;
            if (!pattem.test(tsa)) {
                alert('请输入正确的任务号（如14000SF1-1等）！');
                obj.value = '14000SF1-1';
            }
            if (/[\u2E80-\u9FFF]+/g.test(tsa)) {
                alert("含有汉字！");
                obj.value = '14000SF1-1';
            }
        }

        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }

        function CheckBoxList_Click(sender) {
            var container = sender.parentNode;
            if (container.tagName.toUpperCase() == "TD")
            // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局即为td
            {
                container = container.parentNode.parentNode; // 层次： <table><tr><td><input />
            }
            var chkList = container.getElementsByTagName("input");
            var senderState = sender.checked;
            for (var i = 0; i < chkList.length; i++) {
                chkList[i].checked = false;
            }
            sender.checked = senderState;
        }
        function autoHeight() {
            var iframe = document.getElementById("filework");
            if (iframe.Document) {//ie自有属性
                iframe.style.height = iframe.Document.documentElement.scrollHeight;
            }
            else if (iframe.contentDocument) {//ie,firefox,chrome,opera,safari
                iframe.height = iframe.contentDocument.body.offsetHeight;
            }
        }

        function returnval() {
            var hid = document.getElementById('<%=Hidden.ClientID %>');
            return hid.value;
        }
        function addEvent(eventName, element, fn) {
            if (element.attachEvent) element.attachEvent("on" + eventName, fn);
            else element.addEventListener(eventName, fn, false);
        }

        window.onload = function() {
            var td = document.getElementById("mytd");
            var id = document.getElementById('<%=Hidden.ClientID %>').value;
            var iframeA = document.createElement('iframe');
            iframeA.setAttribute("src", "download.aspx?id=" + id);
            iframeA.setAttribute("id", "filework");
            iframeA.setAttribute("width", "100%");
            iframeA.setAttribute("marginwidth", "0");
            iframeA.setAttribute("frameborder", "0");
            iframeA.setAttribute("scrolling", "no");
            addEvent("load", iframeA, autoHeight);
            td.appendChild(iframeA);
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <asp:HiddenField runat="server" ID="Hidden" />
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="LbtnYes" runat="server" OnClientClick="javascript:return confirm('确认提交吗？');"
                                OnClick="btnYes_Click">
                                <asp:Image ID="Image3" Style="cursor: hand" ToolTip="同意并提交" ImageUrl="~/Assets/icons/positive.gif"
                                    Height="18" Width="18" runat="server" />
                                提交
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="LbtnBack" runat="server" CausesValidation="False" OnClick="btn_back_Click">
                                <asp:Image ID="Image7" Style="cursor: hand" ToolTip="返回" ImageUrl="~/Assets/icons/back.png"
                                    Height="17" Width="17" runat="server" />
                                返回
                            </asp:LinkButton>&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="合同信息" TabIndex="0">
            <HeaderTemplate>
                计划单信息
            </HeaderTemplate>
            <ContentTemplate>
                <div class="box-wrapper1">
                    <div class="box-outer">
                        <div style="width: 85%; margin: 0px auto;">
                            <h2 style="text-align: center; margin-top: 20px">
                                经营计划单
                            </h2>
                            <asp:Panel ID="Panel" runat="server" Width="100%">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                    <tr>
                                        <td style="width: 120px">
                                            编号：
                                        </td>
                                        <td>
                                            <asp:Label ID="CM_ID" runat="server" Width="150px"></asp:Label>
                                        </td>
                                        <td style="width: 120px">
                                            文件号：
                                        </td>
                                        <td>
                                            TJZJ-R-M-04
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            订货单位：
                                        </td>
                                        <td>
                                            <asp:Label ID="CM_COMP" runat="server" Width="150px"></asp:Label>
                                        </td>
                                        <td>
                                            项目名称：
                                        </td>
                                        <td>
                                            <asp:Label ID="CM_PROJ" runat="server" Width="150px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            合同号：
                                        </td>
                                        <td>
                                            <asp:Label ID="CM_CONTR" runat="server" Width="150px"></asp:Label>
                                        </td>
                                        <td>
                                            增加任务号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TSA_ID" runat="server" Width="150px"></asp:TextBox>
                                            <span class="red" runat="server" id="tx2">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            对方合同号
                                        </td>
                                        <td>
                                            <asp:Label ID="CM_DFCONTR" runat="server" Width="150px"></asp:Label>
                                        </td>
                                        <td style="width: 150px">
                                            增加任务号数目：
                                        </td>
                                        <td>
                                            <input id="num" runat="server" value="1" type="text" style="width: 60px; text-align: center"
                                                onblur="CheckNum(this);" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnadd" runat="server" Text="增 加" OnClientClick="return ProAdd();"
                                                OnClick="btnadd_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            备注：
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="TSA_NOTE" ReadOnly="true" runat="server" TextMode="MultiLine" Width="600px"
                                                Height="50px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table width="100%">
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" CssClass="checkBoxCss" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="35px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                                <asp:HiddenField ID="hide" runat="server" Value='<%#Eval("CM_ID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="任务号" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="TSA_ID" Text='<%# Eval("TSA_ID") %>' Width="100px"
                                                                    onblur="checkId(this)" BorderStyle="None" BackColor="Transparent" CssClass="center"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="产品名称" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="TSA_ENGNAME" Text='<%# Eval("TSA_ENGNAME") %>' Width="100px"
                                                                    Height="40px" TextMode="MultiLine" CssClass="center" onkeydown="grControlFocus(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="图号" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="TSA_MAP" Width="100px" TextMode="MultiLine" Height="40px"
                                                                    Text='<%# Eval("TSA_MAP") %>' CssClass="center" onkeydown="grControlFocus(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="TSA_NUMBER" Width="50px" Text='<%# Eval("TSA_NUMBER") %>'
                                                                    CssClass="center" onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;"
                                                                    onchange="CheckNum(this)" onkeydown="grControlFocus(this)" TextMode="MultiLine"
                                                                    Height="21px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="TSA_UNIT" Width="50px" Text='<%# Eval("TSA_UNIT") %>'
                                                                    CssClass="center" onkeydown="grControlFocus(this)" TextMode="MultiLine" Height="21px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="材质" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="TSA_MATERIAL" Height="40px" TextMode="MultiLine"
                                                                    Text='<%# Eval("TSA_MATERIAL") %>' onkeydown="grControlFocus(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="TSA_IDNOTE" Height="40px" TextMode="MultiLine" Text='<%# Eval("TSA_IDNOTE") %>'
                                                                    onkeydown="grControlFocus(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="设备类型" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="TSA_TYPE" runat="server" Width="100px">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField runat="server" ID="Hid_Type" Value='<%#Eval("TSA_TYPE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" Font-Size="Small" />
                                                </asp:GridView>
                                                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                                    没有记录!</asp:Panel>
                                                <br />
                                                <div style="float: left">
                                                    <asp:Button ID="delete" runat="server" Text="删 除" OnClick="delete_Click" /></div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="delete" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Panel1" runat="server" Width="100%">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="white-space: normal">
                                    <tr>
                                        <td>
                                            <div style="width: 130px">
                                                交货日期：</div>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_FHDATE" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            质量标准：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_LEVEL" Text="符合相关的国家标准或行业标准" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            质量校验与验收：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_TEST" Text="按照图纸或相关国家行业标准校验、验收" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            交货地点及运输方式：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_JHADDRESS" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            包装要求：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_BZ" Text="符合国家或行业相关包装标准" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            交货要求：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_JH" runat="server" Text="提供与本合同货物相关的产品合格证书、装箱清单" CssClass="tb"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            油漆要求：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_YQ" runat="server" CssClass="tb"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            买方责任人：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_DUTY" runat="server" CssClass="tb"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            备注：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_NOTE" Text="后附《工作联系单》" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            附件上传：
                                        </td>
                                        <td style="text-align: center;">
                                            <div id="mytd">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            制单人意见：
                                        </td>
                                        <td class="center" colspan="3">
                                            <asp:TextBox ID="txt_zdrYJ" runat="server" Height="40px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px; text-align: left">
                                            任务单抄送：
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel2" runat="server" EnableViewState="False">
                                                <asp:Label ID="errorlb" runat="server" EnableViewState="False" ForeColor="Red" Visible="False"></asp:Label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="Pan_ShenHe" HeaderText="评审信息" TabIndex="1">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:RadioButtonList ID="rblShdj" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                        AutoPostBack="true" OnSelectedIndexChanged="rblShdj_Changed">
                                        <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审核" Value="2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                    任务单审批
                                    <asp:Image ID="ImageAUDIT" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tbody runat="server" id="tb1">
                                <tr>
                                    <td>
                                        <table align="center" width="100%">
                                            <tr style="height: 25px">
                                                <td align="center" style="width: 10%">
                                                    审批人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                    <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand">
                                                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                            align="absmiddle" runat="server" />
                                                        选择
                                                    </asp:HyperLink>
                                                    <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                                        Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect1"
                                                        PopupControlID="pal_select1">
                                                    </asp:PopupControlExtender>
                                                    <asp:Panel ID="pal_select1" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                        <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                            font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                            <tr>
                                                                <td style='background-color: #A8B7EC; color: white;'>
                                                                    <b>选择审核人</b>
                                                                </td>
                                                                <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                    <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                        text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Panel ID="pal_select1_inner" runat="server">
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="right">
                                                                    <asp:Button ID="btn_shr1" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结论
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList ID="rbl_first" RepeatColumns="2" runat="server" Height="20px"
                                                        Enabled="false">
                                                        <asp:ListItem Text="同意" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核时间
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                        ReadOnly="true" Height="42px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                            <tbody runat="server" id="tb2">
                                <tr>
                                    <td>
                                        <table align="center" width="100%">
                                            <tr>
                                                <td align="center" style="width: 10%">
                                                    审批人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                    <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand">
                                                        <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                            align="absmiddle" runat="server" />
                                                        选择
                                                    </asp:HyperLink>
                                                    <asp:PopupControlExtender ID="PopupControlExtender2" CacheDynamicResults="false"
                                                        Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect2"
                                                        PopupControlID="pal_select2">
                                                    </asp:PopupControlExtender>
                                                    <asp:Panel ID="pal_select2" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                        <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                            font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                            <tr>
                                                                <td style='background-color: #A8B7EC; color: white;'>
                                                                    <b>选择审核人</b>
                                                                </td>
                                                                <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                    <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                        text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Panel ID="pal_select2_inner" runat="server">
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="right">
                                                                    <asp:Button ID="btn_shr2" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结论
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList ID="rbl_second" RepeatColumns="2" runat="server" Height="20px"
                                                        Enabled="false">
                                                        <asp:ListItem Text="同意" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核时间
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="second_opinion" runat="server" Height="42px" TextMode="MultiLine"
                                                        ReadOnly="true" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                            <tbody runat="server" id="tb3">
                                <tr>
                                    <td>
                                        <table align="center" width="100%">
                                            <tr>
                                                <td align="center" style="width: 10%">
                                                    审批人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                    <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand">
                                                        <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                            align="absmiddle" runat="server" />
                                                        选择
                                                    </asp:HyperLink>
                                                    <asp:PopupControlExtender ID="PopupControlExtender3" CacheDynamicResults="false"
                                                        Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect3"
                                                        PopupControlID="pal_select3">
                                                    </asp:PopupControlExtender>
                                                    <asp:Panel ID="pal_select3" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                        <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                            font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                            <tr>
                                                                <td style='background-color: #A8B7EC; color: white;'>
                                                                    <b>选择审核人</b>
                                                                </td>
                                                                <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                    <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                        text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Panel ID="pal_select3_inner" runat="server">
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="right">
                                                                    <asp:Button ID="btn_shr3" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结论
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList ID="rbl_third" RepeatColumns="2" runat="server" Height="20px"
                                                        Enabled="false">
                                                        <asp:ListItem Text="同意" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核时间
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="third_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                        ReadOnly="true" Height="42px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="CM_FHNotice.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_FHNOTICE" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    发货通知
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .center
        {
            text-align: center;
        }
    </style>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "0";
            } else {
                var a = $("span", $(obj).parent().parent()).eq(7).text();
                var b = $("span", $(obj).parent().parent()).eq(8).text();
                var s = Number(b) + Number(obj.value);
                if (s > a) {
                    alert("超过数量，请重新输入！");
                    obj.value = "0";
                }
            }
            if (obj.value == "0") {
                $(":checkbox", $(obj).parent().parent()).eq(0).attr("checked", false);
            } else {
                $(":checkbox", $(obj).parent().parent()).eq(0).attr("checked", true);
            }
        }

        function Check() {
            var zg = document.getElementById('<%=firstid.ClientID%>').value;
            if (zg == "") {
                alert("请选择主管");
                event.returnValue = false;
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

        function Test() {
            var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            var bh = document.getElementById("<%=CM_BIANHAO.ClientID%>").value;
            if (!xmlhttp) {
                alert("创建异常");
                return false;
            }
            xmlhttp.open("GET", "Test.ashx?CM_BIANHAO=" + bh + "&ts=" + new Date(), false);
            xmlhttp.onreadystatechange = function() {
                if (xmlhttp.readystate == 4) {//发送请求成功
                    if (xmlhttp.status == 200) { //如果代码是200则成功
                        var json = xmlhttp.responseText; //responseText属性为服务器返回文本
                        if (json == "1") {
                            alert('已存在此编号，请重新输入！');
                            event.returnValue = false;
                        }
                    }
                }
            }
            xmlhttp.send(); //发送请求
            var zg = document.getElementById('<%=firstid.ClientID%>').value;
            if (zg == "") {
                alert("请选择主管");
                event.returnValue = false;
            }
        }

        function allSele() {
            var s = $("#gr :text");
            if ($("#checkAll").attr("checked")) {
                //                $.each(s, function() {
                //                    var a = $("span", $(this).parent().parent()).eq(7).text();
                //                    var b = $("span", $(this).parent().parent()).eq(8).text();
                //                    $(this).val(a - b);
                //                });
                $("#gr :checkbox").attr("checked", true);
            } else {
                //                $.each(s, function(index) {
                //                    $(this).val(0);
                //                });
                $("#gr :checkbox").attr("checked", false);
            };
        }

        var pro;
        function dragVal(obj, dragName) {
            var pattem = /^[0-9]*$/; //数量验证
            var testnum = obj.value;
            var a = $("span", $(obj).parent().parent()).eq(7).text();
            var b = $("span", $(obj).parent().parent()).eq(8).text();
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "0";
            } else {
                var z = Number(b) + Number(obj.value);
                if (z > a) {
                    alert("超过数量，请重新输入！");
                    obj.value = "0";
                }
                var s = $("#gr :text[group='" + dragName + "']");
                $.each(s, function(index) {
                    if (index == "0") {
                        pro = Number(obj.value) / a;
                    } else {
                        var c = $("span", $(this).parent().parent()).eq(7).text();
                        var d = $("span", $(this).parent().parent()).eq(8).text();
                        var ss = Number(d) + Number(c * pro);
                        if (ss > c) {
                            $(this).val(c - d);
                        } else {
                            $(this).val(c * pro);
                        }
                    }
                    if (obj.value == "0") {
                        $(":checkbox", $(this).parent().parent()).eq(0).attr("checked", false);
                    } else {
                        $(":checkbox", $(this).parent().parent()).eq(0).attr("checked", true);
                    }
                });
            }
        }

        function addEvent(eventName, element, fn) {
            if (element.attachEvent) element.attachEvent("on" + eventName, fn);
            else element.addEventListener(eventName, fn, false);
        }

        window.onload = function() {
            var td = document.getElementById("mytd");
            var id = document.getElementById('<%=Hidden.ClientID %>').value;
            var action = document.getElementById('<%=HidAction.ClientID %>').value;
            var iframeA = document.createElement('iframe');
            iframeA.setAttribute("src", "attachment.aspx?id=" + id + "&action=" + action);
            iframeA.setAttribute("id", "filework");
            iframeA.setAttribute("width", "100%");
            iframeA.setAttribute("marginwidth", "0");
            iframeA.setAttribute("frameborder", "0");
            iframeA.setAttribute("scrolling", "no");
            addEvent("load", iframeA, autoHeight);
            td.appendChild(iframeA);
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
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <asp:HiddenField ID="Hidden" runat="server" />
    <asp:HiddenField ID="HidAction" runat="server" />
    <asp:HiddenField ID="HidCSR" runat="server" />
    <asp:Panel ID="Panelall" runat="server">
        <div style="float: right">
            <asp:Button ID="btnsubmit" runat="server" Text="提 交" OnClick="btnsubmit_Click" OnClientClick="Test()" />
            <asp:Button ID="Button1" runat="server" Text="修 改" OnClick="btnsubmit_Click" Visible="false"
                OnClientClick="Check()" />
            <asp:Button ID="Button2" runat="server" Text="签 字" OnClick="btnsubmit_Click" Visible="false" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <br />
        <div class="box-wrapper1">
            <div class="box-outer">
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="合同信息" TabIndex="0">
                        <HeaderTemplate>
                            发货通知信息
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="text-align: center">
                                <div style="text-align: center; margin-top: 20px; padding-top: 10px">
                                    <h2>
                                        发货通知（内部）</h2>
                                </div>
                                <br />
                                <table width="1000px" style="margin: auto">
                                    <tr>
                                        <td align="right" width="10%">
                                            编号：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="CM_BIANHAO" runat="server" CssClass="center"></asp:TextBox>
                                            <asp:HiddenField ID="hid_BianHao" runat="server" />
                                        </td>
                                        <td align="right">
                                            顾客名称：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="CM_CUSNAME" runat="server" CssClass="center"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            制单人：
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="CM_MANCLERK" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            制单时间：
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="CM_ZDTIME" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Panel ID="Panel1" runat="server">
                                    <div style="text-align: right">
                                        合同号：
                                        <asp:TextBox ID="CONTR" runat="server" CssClass="center" Width="100px"></asp:TextBox>
                                        <asp:Button ID="btnadd" runat="server" Text="确 定" OnClick="btnadd_Click" />
                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                    </div>
                                    <br />
                                    <div style="text-align: right">
                                        <%--<asp:CheckBox runat="server" ID="cbxCheckAll" Text="全选" OnCheckedChanged="cbxCheckAll_OnCheckedChanged" />--%>
                                        <input type="checkbox" id="checkAll" onclick="allSele()" /><label for="checkAll">
                                            全选</label>&nbsp; &nbsp; &nbsp; &nbsp;</div>
                                </asp:Panel>
                                <div>
                                    <hr />
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center; width: 95%; margin: auto">
                                            <table id="gr" cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="width: 100%;
                                                white-space: normal">
                                                <asp:Repeater ID="Det_Repeater" runat="server" OnItemDataBound="Det_Repeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr class="tableTitle headcolor" style="text-align: center">
                                                            <td width="50px">
                                                                <strong>序号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>项目名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>合同号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>任务号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>总序</strong>
                                                            </td>
                                                            <td>
                                                                <strong>交货内容</strong>
                                                            </td>
                                                            <td>
                                                                <strong>图号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>已发数目</strong>
                                                            </td>
                                                            <td>
                                                                <strong>发货数目</strong>
                                                            </td>
                                                            <td>
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>备注</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget">
                                                            <td>
                                                                <div style="width: 50px">
                                                                    <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                                    <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                                    </asp:CheckBox>
                                                                    <asp:Label runat="server" ID="lbfh" Font-Size="X-Small" Visible="false" ForeColor="Red"></asp:Label>
                                                                    <asp:HiddenField ID="tid" runat="server" Value='<%#Eval("ID") %>' />
                                                                    <asp:HiddenField ID="sid" runat="server" Value='<%#Eval("CM_ID") %>' />
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="CM_PROJ" runat="server" Width="100px" Text='<%# Eval("CM_PROJ")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="CM_CONTR" runat="server" Width="100px" Text='<%# Eval("CM_CONTR")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="TSA_ID" runat="server" Width="100px" Text='<%# Eval("TSA_ID")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="CM_ID" runat="server" Width="50px" Text='<%# Eval("ID")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="TSA_ENGNAME" runat="server" Width="100px" Text='<%# Eval("TSA_ENGNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="TSA_MAP" runat="server" Width="100px" Text='<%# Eval("TSA_MAP")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="TSA_NUMBER" runat="server" Width="50px" Text='<%# Eval("TSA_NUMBER")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="TSA_YFSM" runat="server" Width="60px" Text='<%# Eval("TSA_YFSM")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%--<asp:TextBox ID="CM_FHNUM" runat="server" Width="50px" Text='<%# Eval("CM_FHNUM") %>'
                                                                    CssClass="center" onblur="CheckNum(this)"></asp:TextBox>--%>
                                                                <asp:TextBox ID="CM_FHNUM" runat="server" Width="50px" Text='<%# Eval("CM_FHNUM") %>'
                                                                    CssClass="center" onblur="CheckNum(this)"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="TSA_UNIT" runat="server" Width="50px" Text='<%# Eval("TSA_UNIT")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="TSA_NOTE" runat="server" Width="150px" Text='<%# Eval("TSA_IDNOTE")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                                没有记录!</asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <br />
                                <table width="850px" style="margin: auto">
                                    <tr>
                                        <td width="30%">
                                            收货单位：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CM_SH" runat="server" Width="300px" CssClass="center"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            交（提）货地点：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CM_JH" runat="server" Width="300px" CssClass="center"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            联系人：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CM_LXR" runat="server" Width="300px" CssClass="center"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            联系方式：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CM_LXFS" runat="server" Width="300px" CssClass="center"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            要求发货时间：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CM_JHTIME" runat="server" Width="300px" CssClass="center" onclick="setday(this)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            要求到货时间：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CM_YQDHSJ" runat="server" Width="300px" CssClass="center" onclick="setday(this)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            备注：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CM_BEIZHU" runat="server" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            附件上传：
                                        </td>
                                        <td style="text-align: center;">
                                            <div id="mytd">
                                            </div>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="抄送" TabIndex="2">
                        <HeaderTemplate>
                            抄送
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Panel ID="Panel2" runat="server" EnableViewState="False">
                            </asp:Panel>
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
                                                    AutoPostBack="true" OnSelectedIndexChanged="rblShdj_Changed" Enabled="false">
                                                    <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="二级审核" Value="2" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                                发货通知审批
                                                <asp:Image ID="ImageAUDIT" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="box-outer">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                        border="1">
                                        <asp:Panel runat="server" ID="tb1">
                                            <tr>
                                                <td>
                                                    <table align="center" width="100%">
                                                        <tr style="height: 25px">
                                                            <td align="center" style="width: 10%">
                                                                审批人
                                                            </td>
                                                            <td style="width: 20%">
                                                                <asp:TextBox ID="txt_first" runat="server" Width="80px" Enabled="false"></asp:TextBox>
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
                                                            <td align="left" style="width: 20%">
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
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="tb2">
                                            <tr>
                                                <td>
                                                    <table align="center" width="100%">
                                                        <tr>
                                                            <td align="center" style="width: 10%">
                                                                审批人
                                                            </td>
                                                            <td style="width: 20%">
                                                                <asp:TextBox ID="txt_second" runat="server" Width="80px" Enabled="false"></asp:TextBox>
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
                                                            <td align="left" style="width: 20%">
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
                                        </asp:Panel>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

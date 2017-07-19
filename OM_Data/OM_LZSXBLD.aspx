<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_LZSXBLD.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_LZSXBLD" Title="离职手续办理单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    离职手续办理单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            border: solid 1px #E0EEE0;
            width: 80%;
            padding: 0;
            margin: 0;
        }
        .tab tr
        {
        }
        .tab tr td
        {
        }
    </style>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();

        }

        function xr2() {
            $("#hidPerson").val("person2");
            SelPersons();
        }

        function xr3() {
            $("#hidPerson").val("person3");
            SelPersons();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=tbxLZ_GZJJZMR.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_GZJJZMRID.ClientID%>").val(r.st_id);
            }
            if (id == "person2") {
                $("#<%=tbxLZ_WPJJZMR.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_WPJJZMRID.ClientID%>").val(r.st_id);
            }
            if (id == "person3") {
                $("#<%=tbxLZ_ZLZMR.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_ZLZMRID.ClientID%>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <table width="100%" style="border-color: #e0eee0;">
                <tr>
                    <td align="right">
                        <asp:Button runat="server" ID="btnSubmit" Text="提交" OnClick="btnSubmit_OnClick" />
                        <asp:Button runat="server" ID="btnQuit" Text="取消(返回)" OnClick="btnQuit_OnClick" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="width: 100%; background-color: #F0F8FF; height:500px; overflow:auto" align="center" id="div1">
        <table width="80%">
            <tr>
                <td align="center" style="width: 100%">
                    <asp:Image runat="server" ID="Image" ImageUrl="~/OM_Data/StyleFile/OM_ZCTOP.jpg" />
                </td>
            </tr>
            <tr>
                <td style="font-size: x-large;" align="center">
                    <strong>离职手续办理单</strong>
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="panZ">
            <asp:Panel runat="server" ID="panJBXX">
                <table width="80%" style="border: solid 1px #E0EEE0">
                    <tr>
                        <td colspan="4">
                            <asp:RadioButtonList runat="server" ID="rblLZ_LX" RepeatDirection="Horizontal">
                                <asp:ListItem Text="办公室人员" Value="0"></asp:ListItem>
                                <asp:ListItem Text="一线班组" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td colspan="4">
                            制单人：
                            <asp:Label runat="server" ID="lbLZ_ZDR"></asp:Label>
                            <asp:HiddenField runat="server" ID="hidLZ_ZDRID" />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;制单时间：
                            <asp:Label runat="server" ID="lbLZ_ZDSJ"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            所在部门
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlLZ_BUMEN" AutoPostBack="true" OnSelectedIndexChanged="ddlLZ_BUMEN_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            姓名
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlLZ_PERSON" AutoPostBack="true" OnSelectedIndexChanged="ddlLZ_PERSON_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            性别
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtLZ_GENDER"></asp:TextBox>
                        </td>
                        <td>
                            班组
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbxLZ_BANZU">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            岗位名称
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbxLZ_GANGWEI"></asp:TextBox>
                        </td>
                        <td>
                            入职时间
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbxLZ_RUZHISJ" class="easyui-datebox" editable="false"></asp:TextBox>
                        </td>
                        <td>
                            申请离职时间
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbxLZ_SQLZSJ" class="easyui-datebox" editable="false"></asp:TextBox>
                        </td>
                        <td>
                            合同年限
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbxLZ_HTNX"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="panBBM">
                <table width="80%">
                    <tr style="background-color: #CDC8B1; height: 30px">
                        <td colspan="2">
                            <strong>一.本部门工作交接</strong>
                            <asp:Label runat="server" ID="lb1" Text="(申请人所在部门)" ForeColor="#ffe486"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="tab" width="80%">
                    <tr>
                        <td colspan="3">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>1、工作方面：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_GZJJZTY" Text="已交接完毕" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_GZJJZTN" Text="未交接完毕" />
                            (原因<asp:TextBox runat="server" ID="tbxLZ_GZWJJYY"></asp:TextBox>)
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 接手人：
                            <asp:TextBox runat="server" ID="tbxLZ_GZJJZMR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_GZJJZMRID" />
                            <asp:Image runat="server" ID="Image1" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                    </tr>
                </table>
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>2、办公用品、设备、工具等方面：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_WPJJZTY" Text="已交接完毕" />
                            包括<asp:TextBox runat="server" ID="tbxLZ_WPJJ"></asp:TextBox>等物品
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_WPJJZTN" Text="未交接完毕" />
                            (原因<asp:TextBox runat="server" ID="tbxLZ_WPWJJYY"></asp:TextBox>)
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 接手人：
                            <asp:TextBox runat="server" ID="tbxLZ_WPJJZMR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_WPJJZMRID" />
                            <asp:Image runat="server" ID="Image2" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                    </tr>
                </table>
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>3、资料方面：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_ZLJJZTY" Text="已交接完毕" />
                            包括<asp:TextBox runat="server" ID="tbxLZ_ZLJJ"></asp:TextBox>等
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_ZLJJZTN" Text="未交接完毕" />
                            (原因<asp:TextBox runat="server" ID="tbxLZ_ZLWJJYY"></asp:TextBox>)
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 接手人：
                            <asp:TextBox runat="server" ID="tbxLZ_ZLZMR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_ZLZMRID" />
                            <asp:Image runat="server" ID="Image3" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr3()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                    </tr>
                </table>
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>4、离职日期确认：</strong><br />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:TextBox runat="server" ID="tbxLZ_RIQI" class="easyui-datebox"
                                editable="false"></asp:TextBox><br />
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 直接领导：
                            <asp:TextBox runat="server" ID="tbxLZ_ZJLL" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_ZJLLZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_ZJLLID" />
            <table width="80%">
                <tr style="background-color: #CDC8B1; height: 30px">
                    <td colspan="2">
                        <strong>二、其他部门业务终止确认</strong>
                    </td>
                </tr>
            </table>
            <asp:Panel runat="server" ID="panSC">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>生产管理部（105室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_SCYWZZZTY" Text="业务已终止" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_SCYWZZZTN" Text="业务未终止" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_SCYWWZZYY"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_SCBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_SCBZZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_SCBZID" />
            <asp:Panel runat="server" ID="panCG">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>采购部（104室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_CGYWZZZTY" Text="业务已终止" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_CGYWZZZTN" Text="业务未终止" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_CGYWWZZYY"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_CGBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_CGBZZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_CGBZID" />
            <asp:Panel runat="server" ID="panJS">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>技术部（203室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_JSYWZZZTY" Text="业务已终止" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_JSYWZZZTN" Text="业务未终止" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_JSYWWZZYY"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_JSBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_JSBZZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_JSBZID" />
            <asp:Panel runat="server" ID="panZL">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>质量部（204室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_ZLYWZZZTY" Text="业务已终止" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_ZLYWZZZTN" Text="业务未终止" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_ZLYWWZZYY"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_ZLBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_ZLBZZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_ZLBZID" />
            <asp:Panel runat="server" ID="panGCS">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>工程师办公室（203室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_GCSYWZZZTY"
                                Text="业务已终止" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_GCSYWZZZTN"
                                Text="业务未终止" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_GCSYWWZZYY"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_GCSBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_GCSBZZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_GCSBZID" />
            <asp:Panel runat="server" ID="panSHIC">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>市场部（303室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_SCBYWZZZTY"
                                Text="业务已终止" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_SCBYWZZZTN"
                                Text="业务未终止" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_SCBYWWZZYY"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_SCBBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_SCBBZZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_SCBBZID" />
            <asp:Panel runat="server" ID="panCW">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>财务部（305室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_CWYWZZZTY" Text="业务已终止" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_CWYWZZZTN" Text="业务未终止" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_CWYWWZZYY"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_CWBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_CWBZZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_CWBZID" />
            <asp:Panel runat="server" ID="panSB">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>安全环保部（201室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 1、劳保用品退回:
                            <asp:CheckBox runat="server" ID="cbxLZ_LBYPGZF" Text="工作服" />
                            <asp:CheckBox runat="server" ID="cbxLZ_LBYPAQM" Text="安全帽" />
                            <asp:CheckBox runat="server" ID="cbxLZ_LBYPGZX" Text="工作鞋" />
                            <asp:CheckBox runat="server" ID="cbxLZ_LBYPQTZT" Text="其他" />
                            <asp:TextBox runat="server" ID="tbxLZ_LBYPQT"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 2、用品折旧费用： 安全帽<asp:TextBox runat="server" ID="tbxLZ_AQMZJFY"></asp:TextBox>元，工作服、鞋<asp:TextBox
                                runat="server" ID="tbxLZ_GZFXZJFY"></asp:TextBox>共计<asp:TextBox runat="server" ID="tbxLZ_ZJFYHJ"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
                            <td align="left">
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 经手人（签字）：<asp:TextBox runat="server" ID="tbxLZ_JSR"></asp:TextBox>
                            </td>
                        </tr>--%>
                    <tr>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长：<asp:TextBox runat="server" ID="tbxLZ_SBBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_SBBZZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_SBBZID" />
            <asp:Panel runat="server" ID="panST">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>食堂业务</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_CFQSZTY" Text="餐费已清算" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_CFQSZTN" Text="餐费未清算" />，欠费<asp:TextBox runat="server"
                                ID="tbxLZ_CFQK"></asp:TextBox>元。
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 食堂经理(签字)：<asp:TextBox runat="server" ID="tbxLZ_STJL"
                                Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_STJLZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_STJLID" />
            <table>
                <tr>
                    <td>
                        备注说明：<br />
                        1、部门工作交接应及时、全面并有相关工作交接单，办公设备、设施需有相关交接或保管人签收保管；<br />
                        2、所涉及业务部门须请负责人审批；<br />
                        3、请在已确认项前打勾，以表示该工作已交接或者完结。
                    </td>
                </tr>
            </table>
            <table width="80%">
                <tr style="background-color: #CDC8B1; height: 30px">
                    <td colspan="2">
                        三、综合办公室终止确认
                    </td>
                </tr>
            </table>
            <asp:Panel runat="server" ID="panCK">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>1、餐卡退还：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_CKTHZTY" Text="是" />,餐补余额<asp:TextBox runat="server"
                                ID="tbxLZ_CBYE"></asp:TextBox>元，收款人<asp:TextBox runat="server" ID="tbxLZ_CBSKR"></asp:TextBox>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_CKTHZTN" Text="否" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_CKWTHYY"></asp:TextBox>）
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_CKGLY" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_CKGLYZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_CKGLYID" />
            <asp:Panel runat="server" ID="panGK">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>2、工卡、《员工手册》退还：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GKTHZTY" Text="是" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GKTHZTN" Text="否" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_GKWTHYY"></asp:TextBox>，丢失补办费<asp:TextBox
                                runat="server" ID="tbxLZ_GKDSPC"></asp:TextBox>元）
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_GKGLY" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_GKGLYZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_GDZCGLYID" />
            <asp:Panel runat="server" ID="panGDZC">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>3、固定资产交接：</strong>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button
                                runat="server" ID="btnGDZCZY" Text="固定资产转移" BackColor="LightGreen" OnClick="btnGDZCZY_OnClick" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button
                                    runat="server" ID="btnCKJL" Text="查看记录" OnClick="btnCKJL_OnClick" BackColor="LightGreen" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                        style="white-space: normal" border="1">
                                        <asp:Repeater ID="rptGDZC" runat="server">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                    <td>
                                                        <strong>序号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>编号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>规格或参数</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用人</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用部门</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用时间</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前存放地点</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用人</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用部门</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用时间</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现存放地点</strong>
                                                    </td>
                                                    <td>
                                                        <strong>经办人</strong>
                                                    </td>
                                                    <td style="width: 180px">
                                                        <strong>转移原因</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                    <td>
                                                        <%#Container.ItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblbh" runat="server" Text='<%#Eval("ZYBIANHAO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsyr" runat="server" Text='<%#Eval("FORMERNAME")%>'></asp:Label><asp:Label
                                                            ID="lblsyrid" runat="server" Visible="false" Text='<%#Eval("FORMERID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsybm" runat="server" Text='<%#Eval("FBM")%>'></asp:Label><asp:Label
                                                            ID="lblsybumenid" runat="server" Visible="false" Text='<%#Eval("FBMID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldate1" runat="server" Text='<%#Eval("TIME1")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblp1" runat="server" Text='<%#Eval("FPLACE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbllatter" runat="server" Text='<%#Eval("LATTERNAME")%>'></asp:Label><asp:Label
                                                            ID="Label2" runat="server" Visible="false" Text='<%#Eval("LATTERID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbllbm" runat="server" Text='<%#Eval("LBM")%>'></asp:Label><asp:Label
                                                            ID="Label4" runat="server" Visible="false" Text='<%#Eval("LBMID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldate2" runat="server" Text='<%#Eval("TIME2")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblp2" runat="server" Text='<%#Eval("LPLACE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbljbr" runat="server" Text='<%#Eval("JBR")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblReason" runat="server" Text='<%#Eval("REASON")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                        <asp:Label runat="server" ID="lbdt1" ForeColor="Red"></asp:Label></asp:Panel>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="txtLZ_GDZCGLY" onfocus="this.blur()" Width="60px"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:RadioButtonList runat="server" ID="rblLZ_GDZCZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_GKGLYID" />
            <asp:Panel runat="server" ID="panZS">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>4、住宿结算：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_TSZTY" Text="是" />
                            ,退宿日期<asp:TextBox runat="server" ID="tbxLZ_TSRQ" class="easyui-datebox" editable="false"></asp:TextBox>
                            ，退回押金<asp:TextBox runat="server" ID="tbxLZ_TSTHYJ"></asp:TextBox>
                            元，水电费<asp:TextBox runat="server" ID="tbxLZ_TSSDF"></asp:TextBox>
                            元。
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_TSZTN" Text="否" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_WTSYY"></asp:TextBox>）
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_TSGLY" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_TSGLYZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_TSGLYID" />
            <asp:Panel runat="server" ID="panDZ">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>5、网络信息处理</strong>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 1）电子设备：<asp:CheckBox runat="server" ID="cbxLZ_DZSBJSJ"
                                Text="计算机" />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBXJ" Text="相机" />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBYDYP" Text="移动硬盘" />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBUP" Text="U盘" />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBQTZT" Text="其他" />
                            <asp:TextBox runat="server" ID="tbxLZ_DZSBQT"></asp:TextBox><br />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBZTY" Text="收回" />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBZTN" Text="转移" />
                            (接受人：<asp:TextBox runat="server" ID="tbxLZ_DZSBJSR"></asp:TextBox>)
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 网络信息：<asp:CheckBox runat="server" ID="cbxLZ_WLXXNWYX"
                                Text="内网邮箱" />
                            <asp:CheckBox runat="server" ID="cbxLZ_WLXXWWYX" Text="外网邮箱" />
                            <asp:CheckBox runat="server" ID="cbxLZ_WLXXXSZH" Text="信使账户" />
                            <asp:CheckBox runat="server" ID="cbxLZ_OAZH" Text="OA账户" />
                            <asp:CheckBox runat="server" ID="cbxLZ_WLXXQTZT" Text="其他" />
                            <asp:TextBox runat="server" ID="tbxLZ_WLXXQT"></asp:TextBox>，全部注销。
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_DWGLY" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <asp:RadioButtonList runat="server" ID="rblLZ_DWGLYZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_DZSBGLYID" />
            <asp:Panel runat="server" ID="panDZSB">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>6、电子设备（非固定资产）交接：</strong>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button
                                runat="server" ID="btnFGDZCZY" Text="非固定资产转移" BackColor="LightGreen" OnClick="btnFGDZCZY_OnClick" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button
                                    runat="server" ID="btnCK1" OnClick="btnCK1_OnClick" Text="查看记录" BackColor="LightGreen" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                        style="white-space: normal" border="1">
                                        <asp:Repeater ID="rptFGDZC" runat="server">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                    <td>
                                                        <strong>序号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>编号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>规格或参数</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用人</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用部门</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用时间</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前存放地点</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用人</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用部门</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用时间</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现存放地点</strong>
                                                    </td>
                                                    <td>
                                                        <strong>经办人</strong>
                                                    </td>
                                                    <td style="width: 180px">
                                                        <strong>转移原因</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                    <td>
                                                        <%#Container.ItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblbh" runat="server" Text='<%#Eval("ZYBIANHAO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsyr" runat="server" Text='<%#Eval("FORMERNAME")%>'></asp:Label><asp:Label
                                                            ID="lblsyrid" runat="server" Visible="false" Text='<%#Eval("FORMERID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsybm" runat="server" Text='<%#Eval("FBM")%>'></asp:Label><asp:Label
                                                            ID="lblsybumenid" runat="server" Visible="false" Text='<%#Eval("FBMID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldate1" runat="server" Text='<%#Eval("TIME1")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblp1" runat="server" Text='<%#Eval("FPLACE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbllatter" runat="server" Text='<%#Eval("LATTERNAME")%>'></asp:Label><asp:Label
                                                            ID="Label2" runat="server" Visible="false" Text='<%#Eval("LATTERID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbllbm" runat="server" Text='<%#Eval("LBM")%>'></asp:Label><asp:Label
                                                            ID="Label4" runat="server" Visible="false" Text='<%#Eval("LBMID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldate2" runat="server" Text='<%#Eval("TIME2")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblp2" runat="server" Text='<%#Eval("LPLACE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbljbr" runat="server" Text='<%#Eval("JBR")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblReason" runat="server" Text='<%#Eval("REASON")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center">
                                        <asp:Label runat="server" ID="lbdt2" ForeColor="Red"></asp:Label></asp:Panel>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="txtLZ_DZSBGLY" onfocus="this.blur()" Width="60px"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:RadioButtonList runat="server" ID="rblLZ_DZSBZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_DWGLYID" />
            <asp:Panel runat="server" ID="panKQ">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>7、考勤确认</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 1）截止日期：<asp:TextBox runat="server" ID="tbxLZ_KQJZRQ"
                                class="easyui-datebox" editable="false"></asp:TextBox>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 2）年休假请假情况：可休
                            <asp:TextBox runat="server" ID="tbxLZ_KQNJKJ"></asp:TextBox>天，已休
                            <asp:TextBox runat="server" ID="tbxLZ_KQNJYX"></asp:TextBox>天
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 考勤员：<asp:TextBox runat="server" ID="tbxLZ_KQGLY" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_KQGLYZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_KQGLYID" />
            <asp:Panel runat="server" ID="panLDGX">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>8、劳动关系解除：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 1）退工办理：截止日期<asp:TextBox runat="server" ID="tbxLZ_TGRQ"
                                class="easyui-datebox" editable="false"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 2)工资结算：截至日期<asp:TextBox runat="server" ID="tbxLZ_GZJSRQ"
                                class="easyui-datebox" editable="false"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_LDGXGLR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_LDGXGLRZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_LDGXGLRID" />
            <asp:Panel runat="server" ID="panSX">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>9、社险缴费截至：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_SXJZZTY" Text="截止日期至：" />
                            <asp:TextBox runat="server" ID="tbxLZ_SXJZRQ" class="easyui-datebox" editable="false"></asp:TextBox>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_SXJZZTN" Text="否" />
                            （原因
                            <asp:TextBox runat="server" ID="tbxLZ_SXWJZYY"></asp:TextBox>）
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_SXGLR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_SXGLRZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_SXGLRID" />
            <asp:Panel runat="server" ID="panGJJ">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>10、公积金缴费截止：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GJJJZZTY" Text="截止日期至：" />
                            <asp:TextBox runat="server" ID="tbxLZ_GJJJZRQ" class="easyui-datebox" editable="false"></asp:TextBox>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GJJJZZTN" Text="否" />
                            （原因
                            <asp:TextBox runat="server" ID="tbxLZ_GJJWJZYY"></asp:TextBox>）
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_GJJGLR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_GJJGLRZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_GJJGLRID" />
            <asp:Panel runat="server" ID="panDA">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>11、人事档案托管情况：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_DAZRSJZT" Text="转入公司账户时间：" />
                            <asp:TextBox runat="server" ID="tbxLZ_DAZRSJ" class="easyui-datebox" editable="false"></asp:TextBox>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_DABGJZSJZT" Text="保管截止日期：" />
                            <asp:TextBox runat="server" ID="tbxLZ_DABGJZSJ" class="easyui-datebox" editable="false"></asp:TextBox>）
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="cbxLZ_DAWZR" Text="未转入" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_DAGLR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_DAGLRZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_DAGLRID" />
            <asp:Panel runat="server" ID="panXX">
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>12、个人信息移除</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GRKQXXYCZT" Text="考勤系统个人信息移除" />；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GRXTXXYCZT" Text="人员信息表个人信息移除" />。
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_GRXXGLY" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_GRXXGLYZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_GRXXGLYID" />
            <asp:Panel runat="server" ID="panC">
                <table class="tab">
                    <tr style="background-color: #CDC8B1; height: 30px">
                        <td>
                            <strong>综合办公室意见</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbSPJL" Text="审批结论："></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:RadioButtonList runat="server" ID="rblLZ_ZHBSPZT" RepeatDirection="Horizontal"
                                Width="80%" RepeatLayout="Flow">
                                <asp:ListItem Text="同意离职&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                                    Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意离职" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbSPJY" Text="审批建议："></asp:Label><br />
                            <asp:TextBox runat="server" ID="tbxLZ_ZHBSPJY" TextMode="MultiLine" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            部长：<asp:TextBox runat="server" ID="tbxLZ_ZHBBZ"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_ZHBBZID" />
            <asp:Panel runat="server" ID="panL">
                <table class="tab">
                    <tr style="background-color: #CDC8B1; height: 30px">
                        <td>
                            <strong>公司领导意见</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbLDSPJL" Text="审批结论："></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:RadioButtonList runat="server" ID="rblLZ_LDSPZT" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="y" Text="同意离职&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"></asp:ListItem>
                                <asp:ListItem Value="n" Text="不同意离职"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbLDSPJY" Text="审批建议："></asp:Label><br />
                            <asp:TextBox runat="server" ID="tbxLZ_LDSPJY" TextMode="MultiLine" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            总经理：<asp:TextBox runat="server" ID="tbxLZ_LD"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_LDID" />
        </asp:Panel>
    </div>
    <div id="win" visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        按部门查询：
                    </td>
                    <td>
                        <input id="dep" name="dept" value="07">
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
                onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;<a class="easyui-linkbutton"
                    data-options="iconCls:'icon-ok',plain:true" onclick="xiuGai();">修改</a>
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>

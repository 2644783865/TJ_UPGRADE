<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_LSZPJH_SQ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_LSZPJH_SQ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    临时招聘计划申请
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            width: 70%;
            border-right: solid 1px #E5E5E5;
            border-bottom: solid 1px #E5E5E5;
        }
        .tab tr
        {
        }
        .tab tr td
        {
            border-left: solid 1px #E5E5E5;
            border-top: solid 1px #E5E5E5;
            text-align: center;
            font-size: larger;
        }
        .tab tr td input
        {
            font-size: medium;
        }
        .tab tr td input[type="text"]
        {
            width: 90%;
            height: 25px;
        }
        #rdlstSeq td
        {
            border: 0;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }
    
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnSubmit" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onserverclick="btnSubmit_onserverclick">提交</a> <a runat="server" id="btnSave" href="#"
                        class="easyui-linkbutton" data-options="iconCls:'icon-add'" onserverclick="btnSave_onserverclick">
                        保存</a> <a runat="server" id="btnBack" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                            onserverclick="btnBack_onserverclick">返回</a>
                <asp:HiddenField runat="server" ID="hidJH_SJID" />
            </div>
        </div>
    </div>
    <div style="width: 100%; background-color: #F0F8FF" align="center" id="div1">
        <asp:Panel runat="server" ID="panJBXX">
            <table width="70%">
                <tr>
                    <td align="center">
                        <asp:Image runat="server" ID="Image0" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-size: x-large;">
                        中材装备集团<asp:Label runat="server" ID="lbJH_SJ" Text=""></asp:Label>临时人员需求计划申报表
                        <asp:HiddenField runat="server" ID="hidLXD_SJID" />
                    </td>
                </tr>
            </table>
            <table class="tab">
                <tr>
                    <td colspan="2" height="25px">
                        单位/部门：中材（天津）重型机械有限公司
                    </td>
                    <td colspan="2">
                        联系电话：86890105
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25px">
                        申请人
                    </td>
                    <td width="25%">
                        <asp:Label runat="server" ID="lbJH_SQR"></asp:Label>
                    </td>
                    <td width="25%">
                        申请时间
                    </td>
                    <td width="25%">
                        <asp:Label runat="server" ID="lbJH_SQSJ"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="tab">
                <tr>
                    <td>
                        部门
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJH_ZPBM"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td rowspan="4" width="30%">
                        需求计划
                    </td>
                    <td width="30%">
                        岗位名称
                    </td>
                    <td width="40%" style="height: 25px">
                        <asp:DropDownList runat="server" ID="ddlJH_GWMC" Height="98%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        需求来源（校园/社会）
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblJH_XQLY" RepeatDirection="Horizontal"
                            BorderWidth="0" Style="margin: auto">
                            <asp:ListItem Text="校园" Value="校园"></asp:ListItem>
                            <asp:ListItem Text="社会" Value="社会"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        招聘方式（集团统一/自主）
                    </td>
                    <td align="center">
                        <asp:RadioButtonList runat="server" ID="rblJH_ZPFS" RepeatDirection="Horizontal"
                            Style="margin: auto">
                            <asp:ListItem Text="集团统一" Value="集团统一"></asp:ListItem>
                            <asp:ListItem Text="自主" Value="自主"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        需求人数
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJH_ZPRS" onblur="CheckNum(this)"></asp:TextBox>人
                    </td>
                </tr>
                <tr>
                    <td>
                        岗位描述
                    </td>
                    <td>
                        岗位名称及工作职责
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJH_ZPGW"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td rowspan="7">
                        任职要求
                    </td>
                    <td>
                        所学专业
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJH_ZPZY"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        毕业院校要求
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJH_ZPYX"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        学历
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJH_ZPXL"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        性别
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblJH_ZPXB" RepeatColumns="2" RepeatDirection="Horizontal"
                            Style="margin: auto">
                            <asp:ListItem Text="男" Value="男"></asp:ListItem>
                            <asp:ListItem Text="女" Value="女"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        年龄
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJH_ZPNL" onblur="CheckNum(this)"></asp:TextBox>岁
                    </td>
                </tr>
                <tr>
                    <td>
                        资格能力要求
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJH_ZPYQ"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        其他要求
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJH_QTYQ"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        希望到岗时间
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtJH_XWDGSJ" class="easyui-datebox" onfocus="this.blur()"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        拟工作地点
                    </td>
                    <td colspan="2">
                        <asp:TextBox runat="server" ID="txtJH_NGZDD"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        其他
                    </td>
                    <td colspan="2">
                        <asp:TextBox runat="server" ID="txtJH_QT"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

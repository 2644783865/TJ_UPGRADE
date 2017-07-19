<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_DorApply.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master"
    Inherits="ZCZJ_DPF.OM_Data.OM_DorApply" Title="住宿管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    住宿申请&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var i;
        array = new Array();
        function SelTechPersons0()//主管领导
        {

            $("#hidPerson").val("zero");
            SelPersons();
        }
        function SelTechPersons1()//主管领导
        {

            $("#hidPerson").val("first");
            SelPersons();
        }
        function SelTechPersons2()//部门领导
        {

            $("#hidPerson").val("second");
            SelPersons();
        }
        function SelTechPersons3()//主管经理
        {

            $("#hidPerson").val("third");
            SelPersons();
        }



        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();

            if (id == "zero") {
                $("#<%=txtshr.ClientID %>").val(r.st_name);
                $("#<%=shrid.ClientID %>").val(r.st_id);
                $("#<%=txtDep.ClientID %>").val(r.dep_name);
                $("#<%=txtPos.ClientID %>").val(r.dep_position);
                $("#<%=txtWno.ClientID %>").val(r.st_workno);
            }
           else if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            else if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            } else if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right" valign="top">
                            <asp:Button ID="btnBack" runat="server" Text="退宿" OnClick="btnBack_OnClick" Visible="false" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_OnClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" Text="返回" OnClick="btnReturn_OnClick" CausesValidation="False" />
                            &nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                <asp:ListItem Text="二级审核" Value="2" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%; overflow: auto;">
            <table width="100%">
                <tr>
                    <td align="center" style="font-weight: bold; font-size: large">
                        中材（天津）重机住宿申请表
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        住宿单号：<asp:Label ID="lblDorCode" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged" />外来人员
                    </td>
                    <td align="right">
                        申请时间：<asp:Label ID="lblDorAddtime" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="Table1" align="center" cellpadding="4" cellspacing="1" runat="server"
                class="toptable grid" border="1" width="100%">
                <tr>
                    <td align="center" style="width: 25%">
                        申请人（来客）姓名
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtshr" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtshr_TextChanged"></asp:TextBox>
                        <input id="shrid" type="text" runat="server" readonly="readonly" style="display: none" />
                        <asp:HyperLink ID="hlSelect0" runat="server" CssClass="hand" OnClick="SelTechPersons0()">
                            <asp:Image ID="AddImage0" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            选择</asp:HyperLink>
                        <span id="span1" runat="server" visible="false" class="Error">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请选择住宿人员！"
                            ControlToValidate="txtshr" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td align="center" style="width: 25%">
                        工号
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtWno" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 25%">
                        所在部门（来客单位）
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtDep" runat="server"></asp:TextBox>
                    </td>
                    <td align="center" style="width: 25%">
                        岗位
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtPos" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 25%">
                        住宿原因
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtRea" runat="server" TextMode="MultiLine" Width="99%" Height="34px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 25%">
                        入住时间
                    </td>
                    <td style="width: 25%">
                        <input id="InDate" runat="server" type="text" class="easyui-datebox" data-options="editable:false" />
                    </td>
                    <td align="center" style="width: 25%">
                        住宿房间号
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtRoom" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        综合管理部
                    </td>
                    <td colspan="3">
                        <table width="100%" cellpadding="4" cellspacing="1" border="1">
                            <tr style="height: 25px">
                                <td align="center" style="width: 10%">
                                    审批人
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                    <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                    <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                        <asp:Image ID="AddImage12" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        选择
                                    </asp:HyperLink>
                                </td>
                                <td align="center" style="width: 10%">
                                    审核结论
                                </td>
                                <td align="center" style="width: 20%">
                                    <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Height="20px"
                                        AutoPostBack="true" OnSelectedIndexChanged="rblfirst_OnSelectedIndexChanged">
                                        <asp:ListItem Text="同意" Value="4"></asp:ListItem>
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
                                        Height="42px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        二级审批
                    </td>
                    <td colspan="3">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                            <tr>
                                <td align="center" style="width: 10%">
                                    审批人
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                    <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                    <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()">
                                        <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        选择
                                    </asp:HyperLink>
                                </td>
                                <td align="center" style="width: 10%">
                                    审核结论
                                </td>
                                <td align="center" style="width: 20%">
                                    <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Height="20px"
                                        AutoPostBack="true" OnSelectedIndexChanged="rblsecond_OnSelectedIndexChanged">
                                        <asp:ListItem Text="同意" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="5"></asp:ListItem>
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
                                        Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        三级审批
                    </td>
                    <td colspan="3">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                            <tr>
                                <td align="center" style="width: 10%">
                                    审批人
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                    <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                    <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelTechPersons3()">
                                        <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        选择
                                    </asp:HyperLink>
                                </td>
                                <td align="center" style="width: 10%">
                                    审核结论
                                </td>
                                <td align="center" style="width: 20%">
                                    <asp:RadioButtonList ID="rblthird" RepeatColumns="2" runat="server" Height="20px"
                                        OnSelectedIndexChanged="rblthird_OnSelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="同意" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="7"></asp:ListItem>
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
                                        Height="42px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
        </div>
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
                        <input id="dep" name="dept" value="03">
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
            保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a>
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>

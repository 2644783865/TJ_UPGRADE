<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="TM_Tech_assign_piliang.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Tech_assign_piliang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    批量分工
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }

        function SelTechPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
        function SelTechPersons3() {
            $("#hidPerson").val("third");
            SelPersons();
        }
        function SelTechPersons4() {
            $("#hidPerson").val("fourth");
            SelPersons();
        }

        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            else if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
                       
             else if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }           
            else if (id == "fourth") {
                $("#<%=txt_fourth.ClientID %>").val(r.st_name);
                $("#<%=fourthid.ClientID %>").val(r.st_id);
            }

            $('#win').dialog('close');
        }
        function auditnull() {
            var person = document.getElementById('<%=txt_first.ClientID %>').value;
            var ddlstatus0 = document.getElementById('ctl00_PrimaryContent_rblstatus_0');
            var ddlstatus1 = document.getElementById('ctl00_PrimaryContent_rblstatus_1');
            var ddlstatus2 = document.getElementById('ctl00_PrimaryContent_rblstatus_2');
            var ddlstatus3 = document.getElementById('ctl00_PrimaryContent_rblstatus_3');
            if (ddlstatus0.checked) {
                var n = confirm('确认添加任务信息吗?');
                if (n == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (ddlstatus1.checked) {
                    if (person == "") {
                        alert('请选择技术负责人!');
                        return false;
                    }
                    else {
                        var i = confirm('确定任务分工吗?');
                        if (i == true) {
                            //alert('qqqqq');
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                }
                else if (ddlstatus2.checked) {
                    var j = confirm('是否确定工程完工?');
                    if (j == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    var k = confirm('是否确定工程停工?');
                    if (k == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }

    </script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                任务信息(带<span style="color: Red;">*</span>号的为必填项)
                            </td>
                            <td align="right">
                                <asp:HyperLink ID="goback" CssClass="hand" runat="server" onclick="history.go(-1);">
                                    <asp:Image ID="Imageback" ImageUrl="~/Assets/icons/back.png" border="0" hspace="2"
                                        Height="16" Width="16" align="absmiddle" runat="server" />返回</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                    border="1">
                    <tr>
                        <td class="tdleft1">
                            任务号:
                        </td>
                        <td class="tdright1" colspan="3">
                            <asp:Label runat="server" ID="tsaid"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                         <td class="tdleft1">
                            技术评审人:
                        </td>
                        <td class="tdright1">
                            <input id="txt_second" type="text" value="" readonly="readonly" runat="server" style="border-style: none" />
                            <input id="secondid" type="text" value="" readonly="readonly" runat="server" style="border-style: none;
                                display: none" /><span style="color: Red;">&nbsp;&nbsp;*</span>
                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="hand" onClick="SelTechPersons2()">
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                选择
                            </asp:HyperLink>
                        </td>
                          <td class="tdleft1">
                            技术负责人:
                        </td>
                        <td class="tdright1">
                            <input id="txt_first" type="text" value="" readonly="readonly" runat="server" style="border-style: none" />
                            <input id="firstid" type="text" value="" readonly="readonly" runat="server" style="border-style: none;
                                display: none" /><span style="color: Red;">&nbsp;&nbsp;*</span>
                            <asp:HyperLink ID="hlSelect" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                选择
                            </asp:HyperLink>
                        </td>
                    </tr>
                                        <tr>
                        <td class="tdleft1">
                            涂装-油漆评审人:
                        </td>
                        <td class="tdright1">
                            <input id="txt_third" type="text" value="" readonly="readonly" runat="server" style="border-style: none" />
                            <input id="thirdid" type="text" value="" readonly="readonly" runat="server" style="border-style: none;
                                display: none" /><span style="color: Red;">&nbsp;&nbsp;*</span>
                            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="hand" onClick="SelTechPersons3()">
                                <asp:Image ID="Image2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                选择
                            </asp:HyperLink>
                        </td>
                        <td class="tdleft1">
                            涂装-油漆负责人:
                        </td>
                        <td class="tdright1">
                            <input id="txt_fourth" type="text" value="" readonly="readonly" runat="server" style="border-style: none" />
                            <input id="fourthid" type="text" value="" readonly="readonly" runat="server" style="border-style: none;
                                display: none" /><span style="color: Red;">&nbsp;&nbsp;*</span>
                            <asp:HyperLink ID="HyperLink3" runat="server" CssClass="hand" onClick="SelTechPersons4()">
                                <asp:Image ID="Image3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                选择
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft1">
                            任务开始时间:
                        </td>
                        <td class="tdright1">
                            <input type="text" runat="server" class="easyui-datebox" id="startdate" />
                        </td>
                        <td class="tdleft1">
                            明细完成时间:
                        </td>
                        <td class="tdright1">
                             <input type="text" runat="server" class="easyui-datebox" id="txtMSComTime" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft1">
                            材料汇总时间:
                        </td>
                        <td class="tdright1">
                           <input type="text" runat="server" class="easyui-datebox" id="txtMPTime" />
                        </td>
                        <td class="tdleft1">
                            工艺完成时间:
                        </td>
                        <td class="tdright1">
                            <input type="text" runat="server" class="easyui-datebox" id="txtTecTime" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft1">
                            涂装完成时间:
                        </td>
                        <td class="tdright1">
                             <input type="text" runat="server" class="easyui-datebox" id="txtTuZhuangTime" />
                        </td>
                        <td class="tdleft1">
                            装箱单完成时间:
                        </td>
                        <td class="tdright1">
                            <input type="text" runat="server" class="easyui-datebox" id="txtZhuangXiangDanTime" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft1">
                            技术任务状态:
                        </td>
                        <td class="tdright1">
                            <asp:RadioButtonList ID="rblstatus" RepeatColumns="4" runat="server">
                                <asp:ListItem Text="待分工" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="进行中" Value="1"></asp:ListItem>
                                <asp:ListItem Text="完工" Value="2"></asp:ListItem>
                                <asp:ListItem Text="停工" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="tdleft1">
                        </td>
                        <td colspan="3">
                            <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_Click"
                                OnClientClick="return auditnull()" />
                            <%-- UseSubmitBehavior="false" OnClientClick="Javascript:if(this.value==' 确 定 '){if(confirm(&quot;任务确定？&quot;)){}else return false;}else {this.value==' 添 加 ';}"--%>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text=" 取 消 " OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
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
        </div>
    </div>
</asp:Content>

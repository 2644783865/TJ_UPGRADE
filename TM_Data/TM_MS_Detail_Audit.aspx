<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="TM_MS_Detail_Audit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_Detail_Audit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    制作明细审批
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var isBianGeng;
        var msId;
        //**********弹出技术部人员子窗口***********************
        var i;
        array = new Array();
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
            $('#win').dialog('close');
        }

        function ShowDetail(index) {
            var newindex = index.substring(0, index.indexOf("-"));
            window.showModalDialog('TM_MSBefore.aspx?LotNum=<%=ms_no.Text %>&MsViewTableName=<%=view_table() %>&NewIndex=' + newindex, '', "dialogHeight:300px;dialogWidth:1000px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
        }

        function Submit() {
            var ok = confirm("提交后不能修改，确认提交吗？");
            if (ok == true) {
                if (document.getElementById("<%=btnsubmit.ClientID%>") == null) {
                    alert("您无权提交！！！");
                    return false;
                }
                else {
                    document.getElementById("<%=btnsubmit.ClientID%>").click();
                    document.getElementById("btnMssubmit").disabled = true;
                }
            }
            return ok;
        }
        function ChangeColor(input) {
            if ($(input).attr("checked") == true) {
                $(input).parent().parent().parent().css("background", "#40E0D0");
                console.info($(input).parent().parent().parent());
            }
            else {
                $(input).parent().parent().parent().css("background", "#FFFFFF");
            }
        }

   
    </script>

    <script type="text/javascript">
        //批注窗口初始化
        $(function() {
            $('#divEditPZ').show().dialog({
                title: '修改',
                width: 260,
                height: 150,
                closed: true,
                cache: false,
                modal: true,
                buttons: '#divBtnPZ'
            });
        });


        $(function() {
            var width = $("#outDiv").width();
            $("#tbList").width(width - 10);
            isBianGeng = $("#<%=ms_no.ClientID %>").html().indexOf('BG') > -1 //判断该批是否是变更

            $("#tbList").datagrid({
                url: 'TM_AjaxHandler.aspx',
                collapsible: true,
                loadMsg: '数据加载中请稍后……',
                queryParams: {
                    method: 'GetMSdata',
                    msId: $("#<%=ms_no.ClientID %>").html()
                },

                resizeHandle: "both",
                collapsible: true,
                fitColumns: true,
                nowrap: false,
                singleSelect: true,
                pageNumber: 25,
                pageSize: 25,
                pageList: [1,25, 40, 60],
                pagination: true,
                rowStyler:setRowBgColor,
                columns: [[
                        {
                            field: 'ck', checkbox: true, width: 30
                        }, {
                            field: 'ms_pizhubeizhu', title: '批注', width: 70, align: "center"
                        }, {
                            field: 'ms_biangengbeizhu', title: '变更备注', width: 70, align: "center", hidden: !isBianGeng
                        }, {
                            field: 'ms_chgpid',title: '变更计划编号', width: 70, align: "center", hidden: true
                        }, {
                            field: 'ms_tuhao', title: '图号', width: 70, align: "center"
                        }, {
                            field: 'ms_zongxu', title: '总序', width: 45, align: "center"
                        }, {
                            field: 'ms_name', title: '名称', width: 60, align: "center"
                        }, {
                            field: 'ms_guige', title: '规格', width: 40, align: "center"
                        }, {
                            field: 'ms_caizhi', title: '材质', width: 60, align: "center"
                        }, {
                            field: 'ms_unum', title: '单数', width: 40 
                        }, {
                            field: 'ms_num', title: '总数', width: 40, align: "center"
                        }, {
                            field: 'ms_tuwght', title: '图纸单重(kg)', width: 60, align: "center"
                        }, {
                            field: 'ms_tutotalwght', title: '图纸总重(kg)', width: 60, align: "center"
                        }, {
                            field: 'ms_mashape', title: '类别', width: 30, align: "center"
                        }, {
                            field: 'ms_techunit', title: '单位', width: 40, align: "center"
                        }, {
                            field: 'ms_yongliang', title: '材料用量', width: 60, align: "center"
                        }, {
                            field: 'ms_matotalwght', title: '材料总重(kg)', width: 60, align: "center"
                        }, {
                            field: 'ms_length', title: '长度', width: 35, align: "center"
                        }, {
                            field: 'ms_width', title: '宽度', width: 35, align: "center"
                        }, {
                            field: 'ms_note', title: '下料备注', width: 50, align: "center"
                        }, {
                            field: 'ms_xialiao', title: '下料方式', width: 50, align: "center"
                        }, {
                            field: 'ms_process', title: '工艺流程', width: 60, align: "center"
                        }, {
                            field: 'ms_ku', title: '库', width: 20, align: "center"
                        }, { 
                            field:'ms_keycoms',title:'是否定尺',width:60,align:"center"
                        },
                        {
                            field: 'ms_allbeizhu', title: '备注', width: 60, align: "center"
                        }
            ]]
            });
        });

        function setRowBgColor(index, row) {
            if (row.ms_chgpid != null && row.ms_chgpid != "") {
                return 'background-color:yellow;color:black;';
            }
        }


        $(function() {
            //更改批注
            $("#<%=btnPiZhuEdit.ClientID %>").click(function() {
                var objdata = $("#tbList").datagrid('getSelections');
                console.log(objdata);
                if (objdata.length == 0) {
                    $.messager.show({
                        title: '提示',
                        msg: '请勾选要更新的记录!',
                        timeout: 5000,
                        showType: 'slide'
                    });
                }
                else {
                    $("#divEditPZ").dialog("open");
                    $("#txtPZ").val(objdata[0].ms_pizhubeizhu);
                    $("#txtBG").val(objdata[0].ms_biangengbeizhu);
                    msId = objdata[0].ms_id;
                    if (!isBianGeng) {
                        $("#txtBG").attr("disabled", "disabled");
                    }
                }
            });
        });

$(function(){
$("#<%=aDeletePZ.ClientID %>").click(function(){
 var msPId = $("#<%=ms_no.ClientID %>").html();
       $.ajax({
                    type: "POST",
                    url: 'TM_AjaxHandler.aspx',
                    data: "method=DeletePiZhu&txtPId=" + msPId,
                    success: function(data) {
                        var objdata = eval("(" + data + ")");
                        $.messager.show({
                            title: '提示',
                            msg: objdata.msg,
                            timeout: 5000,
                            showType: 'slide'
                        });
                        $("#tbList").datagrid('reload');
                    }
                });
});
});

        //保存批注
        function saveBeiZhu() {
            var txtPZ = $("#txtPZ").val();
            var txtBG = $("#txtBG").val();
            $.ajax({
                type: "POST",
                url: 'TM_AjaxHandler.aspx',
                data: "method=EditBeiZhu&txtPZ=" + txtPZ + "&txtBG=" + txtBG + "&msId=" + msId + "&msPId=" + $("#<%=ms_no.ClientID %>").html(),
                success: function(data) {
                    $("#divEditPZ").dialog("close");
                    $("#tbList").datagrid('reload');
                }
            });

        }

        $(function() {


            //更新该批号下的批注,改操作可以找回同意任务号下的之前批次的批注
            $("#<%=aUpdatePZ.ClientID %>").click(function() {
                var txtTsaid = $("#<%=tsa_id.ClientID %>").html();
                var msPId = $("#<%=ms_no.ClientID %>").html();
                $.ajax({
                    type: "POST",
                    url: 'TM_AjaxHandler.aspx',
                    data: "method=UpdatePiZhu&txtTsaid=" + txtTsaid + "&txtPId=" + msPId,
                    success: function(data) {
                        var objdata = eval("(" + data + ")");
                        $.messager.show({
                            title: '提示',
                            msg: objdata.msg,
                            timeout: 5000,
                            showType: 'slide'
                        });
                        $("#tbList").datagrid('reload');
                    }
                });
            });
        });
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0" AutoPostBack="false">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="制作明细" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper" style="width: 100%">
                    <div style="height: 6px" class="box_top" style="width: 100%">
                    </div>
                    <div class="box-outer" style="width: 100%; overflow: scroll">
                        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td style="font-size: large; text-align: center;" colspan="8">
                                    制作明细表
                                </td>
                                <td align="right">
                                    <asp:Image ID="Image3" ToolTip="返回上一页" CssClass="hand" Height="16" Width="16" runat="server"
                                        onclick="history.go(-1);" ImageUrl="~/Assets/icons/back.png" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 8%" align="right" class="back">
                                    任务号:
                                </td>
                                <td style="width: 15%">
                                    <asp:Label ID="tsa_id" runat="server" Width="100%" />
                                </td>
                                <td style="width: 8%" align="right">
                                    合同号:
                                </td>
                                <td style="width: 14%">
                                    <asp:Label ID="lab_contract" runat="server" Width="100%" />
                                </td>
                                <td style="width: 8%" align="right">
                                    项目名称:
                                </td>
                                <td style="width: 14%">
                                    <asp:Label ID="lab_project" runat="server" Width="100%" />
                                </td>
                                <td style="width: 8%" align="right" colspan="2">
                                    计划编号:
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="ms_no" runat="server" Width="100%" />
                                </td>
                                <caption>
                                    <input id="pro_id" runat="server" readonly="readonly" style="display: none" type="text">
                                    <input id="eng_type" runat="server" readonly="readonly" style="display: none" type="text">
                                </caption>
                            </tr>
                            <tr align="right">
                                <td>
                                    设备名称:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lab_engname" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width: 8%" align="right">
                                    批准日期:
                                </td>
                                <td>
                                    <asp:Label ID="txt_approval" runat="server" Width="100%" />
                                </td>
                                <td style="width: 8%" align="right">
                                    编制日期:
                                </td>
                                <td>
                                    <asp:Label ID="txt_plandate" runat="server" Width="100%" />
                                </td>
                                <td>
                                    图号：
                                </td>
                                <td align="left" colspan="2">
                                    <asp:Label ID="lblTuhao" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    材料利用率
                                </td>
                                <td>
                                    <asp:Label ID="lblMpUsagePercent" runat="server" Width="100%" />
                                </td>
                                <td align="center" colspan="3">
                                    <a href="#" id="btnPiZhuEdit" runat="server">
                                        <img src="~/Assets/images/create.gif" style="border: 0px;" runat="server" />修改备注
                                    </a>&nbsp;&nbsp;&nbsp; <a href="#" id="aUpdatePZ" runat="server">
                                        <img id="Img1" src="~/Assets/images/create.gif" style="border: 0px;" runat="server" />更新批注
                                    </a>&nbsp;&nbsp;&nbsp; <a href="#" id="aDeletePZ" runat="server">
                                        <img id="Img2" src="~/Assets/images/create.gif" style="border: 0px;" runat="server" />删除批注
                                    </a>&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_OnClick">
                                        <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />导出Excel</asp:LinkButton>
                                </td>
                                <td colspan="4">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="outDiv" class="box-outer" style="width: 98%">
                        <table id="tbList" style="height: 400px;">
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="审  核" TabIndex="1">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;
                                    <input id="btnMssubmit" type="button" value="提 交" onclick="return Submit();" />
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="hidden" OnClick="btnsubmit_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox ID="ckbMessage" Checked="true" runat="server" />&nbsp;邮件提醒
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: large; text-align: center; height: 43px" colspan="2">
                                    制作明细表
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td style="width: 10%" align="center">
                                    项目名称
                                </td>
                                <td style="width: 40%">
                                    <asp:Label ID="proname" runat="server" Width="100%" />
                                </td>
                                <td style="width: 10%" align="center">
                                    设备名称
                                </td>
                                <td style="width: 40%">
                                    <asp:Label ID="engname" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    编制
                                </td>
                                <td>
                                    <asp:Label ID="txt_editor" runat="server" Width="100%"></asp:Label>
                                </td>
                                <input id="editorid" type="text" runat="server" readonly="readonly" style="display: none" />
                                <td align="center">
                                    编制日期
                                </td>
                                <td>
                                    <asp:Label ID="plandate" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    主管审核
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
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()"
                                                    Visible="false">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Height="20px">
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
                                    部门领导
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
                                                <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Height="20px">
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
                                    主管经理
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
                                                <asp:RadioButtonList ID="rblthird" RepeatColumns="2" runat="server" Height="20px">
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
                        <input id="hidPerson" type="hidden" value="" />
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
                        </div>
                    </div>
                    <div>
                        <div id="divEditPZ" visible="false">
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <strong>批注内容：</strong>
                                        </td>
                                        <td>
                                            <input id="txtPZ" type="text">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>变更备注：</strong>
                                        </td>
                                        <td>
                                            <input id="txtBG" type="text">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div id="divBtnPZ" style="text-align: right" visible="false">
                            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="saveBeiZhu();">
                                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                                    onclick="javascript:$('#divEditPZ').dialog('close')">取消</a>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>

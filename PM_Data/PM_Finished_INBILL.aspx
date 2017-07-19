<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_Finished_INBILL.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Finished_INBILL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>

    <script type="text/javascript">

        $(function() {
            $("#btnConfirm").click(function() {
                var taskID = $("#<%=txt_tsa.ClientID %>").val();
                if (taskID != "") {
                    $("#tbList").treegrid({
                        url: 'PM_AjaxFinished.aspx',
                        collapsible: true,
                        loadMsg: '数据加载中请稍后……',
                        queryParams: {
                            method: 'GetBomData',
                            taskId: taskID
                        },
                        idField: "bm_zongxu",
                        treeField: "bm_zongxu",
                        fitColumns: true,
                        resizeHandle: "both",
                        singleSelect: false,
                        columns: [[
                        {
                            field: 'ck', checkbox: true, width: 30
                        },
                         {
                             field: 'bm_zongxu', title: '总序', width: 40, align: "center"
                         },
                {
                    field: 'bm_tuhao', title: '图号', width: 40, align: "center"
                },

                {
                    field: 'bm_chaname', title: '名称', width: 50
                }, { field: 'bm_tuunitwght', title: '单重' }, { field: 'bm_number', title: '总数' }, { field: 'bm_yrknum', title: '已入库数量' }, { field: 'bm_ku', title: '库' }
            ]]
                    });
                }
            });

        });

        $(function() {
            $("#btnSave").click(function() {
                var objdata = $("#tbList").treegrid('getSelections');
                var zongxu = '';
                console.log(objdata);
                if (objdata.length == 0) {

                    $.messager.show({
                        title: '提示',
                        msg: '请勾选要入库的记录!',
                        timeout: 5000,
                        showType: 'slide'
                    });
                }
                else if (objdata[0].bm_yrknum == objdata[0].bm_number) {
                    $.messager.show({
                        title: '提示',
                        msg: '该成品已入库或正在审核，请勿重复入库!',
                        timeout: 5000,
                        showType: 'slide'
                    });
                }
                else {
                    for (var i = 0; i < objdata.length; i++) {
                        zongxu += objdata[i].bm_zongxu + ',';
                    }
                    $.ajax({
                        type: "POST",
                        url: "PM_AjaxFinished.aspx",
                        data: "method=ProductIn&taskId=" + $("#<%=txt_tsa.ClientID %>").val() + "&zongxu=" + zongxu + "&docNum=" + $("#<%=lbi_docnum.ClientID %>").html() + "&sqr=" + $("#<%=cob_sqren.ClientID %>").val() + "&fzr=" + $("#<%=cob_sqren.ClientID %>").val(),
                        success: function(data) {
                            var dataObj = eval("(" + data + ")");
                            console.log(dataObj.msg);
                            $.messager.show({
                                title: '提示',
                                msg: dataObj.msg,
                                timeout: 5000,
                                showType: 'slide'
                            });
                            $("#tbList").treegrid('reload');
                            $("#btnSave").attr("disabled", "disabled");
                        }
                    });
                }
            });

        });
    
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

        <ContentTemplate>
            <div class="RightContent" style="overflow: hidden">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <input type="button" id="btnSave" value="提交" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btn_back" runat="server" Text="返回" OnClientClick="history.go(-1)" />
                                        &nbsp; &nbsp;&nbsp; &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" style="font-size: x-large; text-align: center;">
                                        成品入库单
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 50%">
                                        收料日期：<asp:Label ID="lblInDate" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 50%">
                                        入库单号：<asp:Label ID="lbi_docnum" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="float: right;">
                        <asp:Label ID="bhgts" runat="server" ForeColor="Red"></asp:Label>
                         <br />
                         </div>
                        <br />
                        <div style="border: 1px solid #000000; height: 330px; margin-top:5px">
                        <div style="border: 1px solid #000000; height: 330px">
                            <table id="tab" width="100%" class="nowrap cptable fullwidth">
                                <tr>
                                    <td>
                                        请输入任务单号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_tsa" runat="server" AutoPostBack="true" OnTextChanged="sfczbhgx"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_tsa"
                                            ServicePath="PM_AutoComplete.asmx" CompletionSetCount="15" MinimumPrefixLength="1"
                                            CompletionInterval="10" ServiceMethod="GetCompletebytsaid" FirstRowSelected="true"
                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        添加备注：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_note" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <%-- <asp:Button ID="btn_confirm" runat="server" Text="生成一级明细" OnClick="btn_confirm_OnClick" />--%>
                                        <input type="button" id="btnConfirm" value="生成明细"/>
                                    </td>
                                </tr>
                            </table>
                            <table id="tbList" style="height: 290px">
                            </table>
                        </div>
                        <div>
                            <table width="100%" style="text-align: center">
                                <tr>
                                    <td>
                                        负责人:
                                        <asp:DropDownList ID="cob_fuziren" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        申请人:
                                        <asp:DropDownList ID="cob_sqren" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        制单人:<asp:TextBox ID="TextBoxexecutor" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
</asp:Content>

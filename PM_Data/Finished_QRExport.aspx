<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="Finished_QRExport.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.Finished_QRExport" Title="无标题页" %>
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

        function btnExport_ClientClick()
        {
            var zongxus="";
            var objdata = $("#tbList").treegrid('getSelections');
            if (objdata.length == 0) 
            {
                $.messager.show({
                    title: '提示',
                    msg: '请勾选要导出的记录!',
                    timeout: 5000,
                    showType: 'slide'
                });
            }
            else 
            {
                for (var i = 0; i < objdata.length; i++) {
                    zongxus += objdata[i].bm_zongxu + ';';
                }
                $("#<%=txt_zongxulist.ClientID %>").val(zongxus);
                sleep(1000);
            }
        }
        
        function sleep(d){
            for(var t = Date.now();Date.now() - t <= d;);
        }
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
                                        <asp:Button ID="btnExport" runat="server" Text="导出" OnClientClick="btnExport_ClientClick()" OnClick="btnExport_Click" />
                                        &nbsp; &nbsp;&nbsp; &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
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
                                        已选总序列表：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_zongxulist" Width="200px" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <input type="button" id="btnConfirm" value="生成明细"/>
                                    </td>
                                </tr>
                            </table>
                            <table id="tbList" style="height: 290px">
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
</asp:Content>

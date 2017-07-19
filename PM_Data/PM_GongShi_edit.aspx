<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_GongShi_edit.aspx.cs"
    Inherits="ZCZJ_DPF.PM_Data.PM_GongShi_edit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base id="goDownload" target="_self" />
    <title>PMS项目管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
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
</head>
<body>
    <form id="form1" runat="server">
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                核对任务号
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
       <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                            <tr>
                                <td align="right" style="width: 150px">
                                    顾客名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="GS_CUSNAME" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 150px">
                                    合同号：
                                </td>
                                <td>
                                    <asp:TextBox ID="GS_CONTR" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 150px">
                                    任务单号：
                                </td>
                                <td>
                                    <asp:TextBox ID="GS_TSAID" runat="server" Width="200px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="GS_TSAID"
                                        ServicePath="PM_Data_AutoComplete.asmx" CompletionSetCount="15" MinimumPrefixLength="1"
                                        CompletionInterval="10" ServiceMethod="GetCompletebytsaid" FirstRowSelected="true"
                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                    </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 150px">
                                    图号：
                                </td>
                                <td>
                                    <asp:TextBox ID="GS_TUHAO" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 150px">
                                    图名：
                                </td>
                                <td width="200">
                                    <asp:TextBox ID="GS_TUMING" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 150px">
                                    备注：
                                </td>
                                <td>
                                    <asp:TextBox ID="GS_NOTE" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 150px">
                                    总计工时：
                                </td>
                                <td width="200px">
                                    <asp:TextBox ID="GS_HOURS" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <asp:Button ID="btnupdate" runat="server" Text="修改" OnClick="btnupdate_Click" />
                                    <input type="button" id="btn_Back" value="返回" onclick="window.close()" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

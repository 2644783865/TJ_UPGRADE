<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TBMP_Supply_fayun.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.PM_Data.TBMP_Supply_fayun" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    选择供货商
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
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
            width: 200px !important;
            font-size: small;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
            font-size: small;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            font-size: small;
        }
    </style>

    <script type="text/javascript">

        String.prototype.Trim = function() {
            return this.replace(/\s+/g, "");
        }

        function providerchanged(i, n) {
            var provider;
            var providerid;
            var providernm;
            var rank;
            table = document.getElementById("tab1");
            tr = table.getElementsByTagName("tr");

            provider = tr[n].getElementsByTagName("td")[i].getElementsByTagName("input")[0].value.Trim();
            if (provider.indexOf("|") > 0) {
                var strs = provider.split("|");
                if (strs.length == 3) {
                    tr[n].getElementsByTagName("td")[i].getElementsByTagName("input")[2].value = provider;
                }
            }
            else if (provider == "") {
                tr[n].getElementsByTagName("td")[i].getElementsByTagName("input")[2].value = "";
            }
            if (tr[n].getElementsByTagName("td")[i].getElementsByTagName("input")[2].value == "") {
                providerid = "";
                providernm = "";
                rank = "";
            }
            else {
                var strs2 = tr[n].getElementsByTagName("td")[i].getElementsByTagName("input")[2].value.split("|");
                providerid = strs2[0];
                providernm = strs2[1];
                rank = strs2[2];
            }
            tr[n].getElementsByTagName("td")[i].getElementsByTagName("input")[1].value = providerid;
            tr[n].getElementsByTagName("td")[i].getElementsByTagName("input")[0].value = providernm;
            tr[n].getElementsByTagName("td")[i].getElementsByTagName("span")[0].innerHTML = rank;
        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:Panel ID="PanelCondition1" runat="server" Width="60%">
        <table id="tab1" width="100%">
            <tr>
                <td>
                    供应商1
                </td>
                <td>
                    供应商2
                </td>
                <td>
                    供应商3
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="PM_SUPPLIERANAME" runat="server" Text="" onblur="providerchanged(0,1)"></asp:TextBox>
                    <asp:Label ID="LbA_lei" runat="server" Text="" Style="display: none"></asp:Label>
                    <asp:TextBox ID="PM_SUPPLIERAID" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="PM_SUPPLIERA" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="PM_SUPPLIERANAME"
                        ServicePath="~/PM_Data/PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                        CompletionInterval="10" ServiceMethod="Getfayun" FirstRowSelected="true"
                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                    </cc1:AutoCompleteExtender>
                </td>
                <td>
                    <asp:TextBox ID="PM_SUPPLIERBNAME" runat="server" Text="" onblur="providerchanged(1,1)"></asp:TextBox>
                    <asp:Label ID="LbB_lei" runat="server" Text="" Style="display: none"></asp:Label>
                    <asp:TextBox ID="PM_SUPPLIERBID" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="PM_SUPPLIERB" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="PM_SUPPLIERBNAME"
                        ServicePath="~/PM_Data/PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                        CompletionInterval="10" ServiceMethod="Getfayun" FirstRowSelected="true"
                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                    </cc1:AutoCompleteExtender>
                </td>
                <td>
                    <asp:TextBox ID="PM_SUPPLIERCNAME" runat="server" Text="" onblur="providerchanged(2,1)"></asp:TextBox>
                    <asp:Label ID="LbC_lei" runat="server" Text="" Style="display: none"></asp:Label>
                    <asp:TextBox ID="PM_SUPPLIERCID" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="PM_SUPPLIERC" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="PM_SUPPLIERCNAME"
                        ServicePath="~/PM_Data/PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                        CompletionInterval="10" ServiceMethod="Getfayun" FirstRowSelected="true"
                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                    </cc1:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td>
                    供应商4
                </td>
                <td>
                    供应商5
                </td>
                <td>
                    供应商6
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="PM_SUPPLIERDNAME" runat="server" Text="" onblur="providerchanged(0,3)"></asp:TextBox>
                    <asp:Label ID="LbD_lei" runat="server" Text="" Style="display: none"></asp:Label>
                    <asp:TextBox ID="PM_SUPPLIERDID" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="PM_SUPPLIERD" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="PM_SUPPLIERDNAME"
                        ServicePath="~/PM_Data/PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                        CompletionInterval="10" ServiceMethod="Getfayun" FirstRowSelected="true"
                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                    </cc1:AutoCompleteExtender>
                </td>
                <td>
                    <asp:TextBox ID="PM_SUPPLIERENAME" runat="server" Text="" onblur="providerchanged(1,3)"></asp:TextBox>
                    <asp:Label ID="LbE_lei" runat="server" Text="" Style="display: none"></asp:Label>
                    <asp:TextBox ID="PM_SUPPLIEREID" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="PM_SUPPLIERE" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="PM_SUPPLIERENAME"
                        ServicePath="~/PM_Data/PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                        CompletionInterval="10" ServiceMethod="Getfayun" FirstRowSelected="true"
                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                    </cc1:AutoCompleteExtender>
                </td>
                <td>
                    <asp:TextBox ID="PM_SUPPLIERFNAME" runat="server" Text="" onblur="providerchanged(2,3)"></asp:TextBox>
                    <asp:Label ID="LbF_lei" runat="server" Text="" Style="display: none"></asp:Label>
                    <asp:TextBox ID="PM_SUPPLIERFID" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="PM_SUPPLIERF" runat="server" Text="" Style="display: none"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" TargetControlID="PM_SUPPLIERFNAME"
                        ServicePath="~/PM_Data/PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                        CompletionInterval="10" ServiceMethod="Getfayun" FirstRowSelected="true"
                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                    </cc1:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                </td>
            </tr>
        </table>
        <asp:Button ID="btnClose" runat="server" Text="确定" OnClick="btnClose_Click1" />
    </asp:Panel>
</asp:Content>
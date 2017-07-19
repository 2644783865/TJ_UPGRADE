<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_LZFGDZC.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_LZFGDZC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    非固定资产转移&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        $(function() {
            //console.log($("select[name*=ddl_bumen]"));
            $("select[name*=ddl_bumen2]").change(function() {
                $ddlDep = $(this);
                var depId = $(this).val();
                $.ajax({
                    url: 'OM_AjaxHandler.aspx',
                    type: 'post',
                    data: { method: 'FindDepPeo', Id: depId },
                    dataType: 'json',
                    success: function(data) {
                        //console.log($ddlDep.parent().parent().find("select[name*=ddl_user]"));
                        var $ddlUser = $ddlDep.parent().parent().find("select[name*=ddl_user2]");
                        $ddlUser.empty();
                        for (i = 0; i < data.length; i++) {
                            var option = $("<option ></option>").val(data[i].st_id).html(data[i].st_name);
                            $ddlUser.append(option);

                        }
                        $ddlUser.parent().find("input[type=hidden][name*=hidSyr2]").val($ddlUser.find(":selected").text());
                        $ddlUser.parent().find("input[type=hidden][name*=hidSyrId2]").val($ddlUser.val());

                    }
                });
            });
        });
        $(function() {
            $("select[name*=ddl_user2]").change(function() {
                $(this).parent().find("input[type=hidden][name*=hidSyr2]").val($(this).find(":selected").text());
                $(this).parent().find("input[type=hidden][name*=hidSyrId2]").val($(this).val());
                // console.log($("input[type=hidden][name*=hidSyr]").val());
                //console.log($("input[type=hidden][name*=hidSyrId]").val());
            });
        });     
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btn_submit" Text="确定转移" OnClick="btn_submit_click" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%; overflow: auto">
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                    border="1">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_OnItemDataBound">
                        <HeaderTemplate>
                            <tr>
                                <td>
                                    <strong>非固定资产转移单</strong>
                                </td>
                            </tr>
                            <tr align="center" class="tableTitle">
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
                                    <strong>转出人</strong>
                                </td>
                                <td>
                                    <strong>转出部门</strong>
                                </td>
                                <td>
                                    <strong>转出地点</strong>
                                </td>
                                <td>
                                    <strong>使用时间</strong>
                                </td>
                                <td>
                                    <strong>转入部门</strong>
                                </td>
                                <td>
                                    <strong>转入人</strong>
                                </td>
                                <td>
                                    <strong>转入时间</strong>
                                </td>
                                <td>
                                    <strong>转入地点</strong>
                                </td>
                                <td>
                                    <strong>原因</strong>
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                <td>
                                    <%#Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <asp:Label ID="lblbh" runat="server" Text='<%#Eval("BH")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblsyr" runat="server" Text='<%#Eval("SYR")%>'></asp:Label><asp:Label
                                        ID="lblsyrid" Visible="false" runat="server" Text='<%#Eval("SYRID")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblsybm" runat="server" Text='<%#Eval("BUMEN")%>'></asp:Label>
                                    <asp:Label ID="syrbmid" runat="server" Text='<%#Eval("BUMENID")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblplace1" runat="server" Text='<%#Eval("PLACE")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbldate" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_bumen2" runat="server" name="ddl">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hfbumen2" runat="server" />
                                    <%--<asp:TextBox ID="txtzrbm" runat="server" ></asp:TextBox>--%>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_user2" runat="server" name="ddl">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hfSYR2" runat="server" />
                                    <input type="hidden" runat="server" id="hidSyr2" name="hid" />
                                    <input type="hidden" runat="server" id="hidSyrId2" name="hid" />
                                    <%--<asp:TextBox ID="txtzrr" runat="server" ></asp:TextBox>--%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdate2" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtplace2" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtreason" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

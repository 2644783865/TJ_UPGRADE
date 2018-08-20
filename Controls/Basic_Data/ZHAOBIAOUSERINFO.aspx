<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="ZHAOBIAOUSERINFO.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.ZHAOBIAOUSERINFO" Title="招标供应商用户信息管理" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    供应商用户信息管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <script type="text/javascript">
    
        //初始化弹窗
        $(function() {
            $("#winedituser").show().dialog({
                title: '修改用户信息',
                width: 280,
                height: 200,
                closed: true,
                cache: false,
                modal: true,
                buttons: '#divedituser'
            });
        });
        //打开务弹窗
        function showedituser(obj) {
            var supplyname="";
            var supplyid="";
            var username = "";
            var userpassword = "";
            supplyname=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            supplyid=obj.parentNode.parentNode.getElementsByTagName("td")[0].getElementsByTagName("input")[0].value;
            username=obj.parentNode.parentNode.getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;
            userpassword=obj.parentNode.parentNode.getElementsByTagName("td")[4].getElementsByTagName("input")[0].value;
            $("#txtsupplyname").val(supplyname);
            $("#hidsupplyid").val(supplyid);
            $("#txtusername").val(username);
            $("#txtuserpassword").val(userpassword);
            $("#winedituser").dialog("open"); 
        }
        function saveedituser() {
            var supplyid=$("#hidsupplyid").val();
            var username = $("#txtusername").val();
            var userpassword = $("#txtuserpassword").val();
            if (username != "" && userpassword != "") {
                $.ajax({
                    type: "POST",
                    url: "BD_AjaxHandler.aspx",
                    data: { "supplyid": supplyid, "username": username,"userpassword":userpassword, "method": "edituser" },
                    success: function(msg) {
                        if (msg == "true") {
                            alert("修改成功！");
                        } else {
                            alert("修改失败，请勿插入重复数据");

                        }
                        $("#txtusername").val("");
                        $("#txtuserpassword").val("");
                        $("#winedituser").dialog("close");
                        window.location.reload();
                    }
                });
            }
        }
    </script>
    
    
    <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                    </div>
                </div>
                <div class="box-wrapper">
                    <div align="left" class="box-outer" style="width: 100%;">
                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; 
                            名称/助记码：<asp:TextBox ID="tb_csname" runat="server"></asp:TextBox>
                            <asp:Button ID="btn_select" runat="server" Text="查询" OnClick="btn_select_Click" />
                        <br />
                        <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                            <table width="600px" align="center" cellpadding="4" cellspacing="1" class="toptable grid nowrap"
                                border="1">
                                <asp:Repeater ID="rptTBCS_CUSUPINFO" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle">
                                            <td width="10px">
                                                <strong>序号</strong>
                                            </td>
                                            <td>
                                                <strong>公司名称</strong>
                                            </td>
                                            <td>
                                                <strong>助记码</strong>
                                            </td>
                                            <td>
                                                <strong>用户名</strong>
                                            </td>
                                            <td>
                                                <strong>密码</strong>
                                            </td>
                                            <td>
                                                <strong>修改</strong>
                                            </td>
                                            <td>
                                                <strong>删除</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" style="text-align: left">
                                            <td width="30px">
                                                <input id="hidcsid" runat="server" type="hidden" value='<%#Eval("CS_CODE")%>' />
                                                <asp:Label ID="lblCS_CODE" runat="server" Visible="false" Text='<%#Eval("CS_CODE")%>'></asp:Label>
                                                <%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>&nbsp;
                                            </td>
                                            <td>
                                                <input id="hidcsname" runat="server" type="hidden" value='<%#Eval("CS_NAME")%>' />
                                                <%#Eval("CS_NAME")%>
                                            </td>
                                            <td>
                                                <%#Eval("CS_HRCODE")%>
                                            </td>
                                            <td>
                                                <input id="hidsupplyusername" runat="server" type="hidden" value='<%#Eval("supplyusername")%>' />
                                                <%#Eval("supplyusername")%>
                                            </td>
                                            <td>
                                                <input id="hidsupplypassword" runat="server" type="hidden" value='<%#Eval("supplypassword")%>' />
                                                <%#Eval("supplypassword")%>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="Hypedituser" runat="server" onClick="showedituser(this)">
                                                    <asp:Image ID="Image5" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    修改
                                                </asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="Ibtn_delete" runat="server" src="../assets/icons/delete.gif"
                                                    alt="删除" Style="border-width: 0px;" OnCommand="deleteuser_Click" CommandName='<%#Eval("CS_CODE")%>'
                                                    OnClientClick="return confirm(&quot;是否删除该条信息？&quot;)" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="NoDataPanel" runat="server">
                                <p style="color: Red; text-align: center">
                                    没有记录!
                                </p>
                            </asp:Panel>
                        </div>
                        <asp:Panel ID="Panel1" runat="server">
                            <table width="100%">
                                <tr style="text-align: right;">
                                    <td>
                                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <p style="text-align: center">
                            <asp:Label ID="lblTotalNumber" runat="server" Text=""></asp:Label></p>
                    </div>
                </div>
            </div>
            
            
            
             <div id="winedituser" style="display: none">
                <form method="post" id="formgztask">
                <div>
                    <table>
                        <tr>
                            <td>
                                <strong>公司名称</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <input id="txtsupplyname" type="text" onblur="this.blur()" />
                                <input type="hidden" id="hidsupplyid" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>用户名</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <input id="txtusername" type="text" class="easyui-validatebox" data-options="required:true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>密码</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <input id="txtuserpassword" type="text" class="easyui-validatebox" data-options="required:true" />
                            </td>
                        </tr>
                    </table>
                </div>
                </form>
            </div>
            <div id="divedituser" style="text-align: right; display: none">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="saveedituser()">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#winedituser').dialog('close')">取消</a>
            </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="QROut_Finished_List.aspx.cs" Inherits="ZCZJ_DPF.QR_Interface.QROut_Finished_List" Title="扫码出库管理" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
扫码出库管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function PushConfirm() {
            var retVal = confirm("确定将所选定成品下推生成出库单？");
            return retVal;
        }

        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnPush" runat="server" Text="出库" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()" />
                            <asp:HiddenField ID="docnum" runat="server" />
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
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle">
                                <td>
                                    <strong>序号</strong>
                                </td>
                                
                                <td>
                                    <strong>任务号</strong>
                                </td>
                                <td>
                                    <strong>扫码时间</strong>
                                </td>
                                <td>
                                    <strong>扫码人</strong>
                                </td>
                                
                                <td>
                                    <strong>编号</strong>
                                </td>
                                <td>
                                    <strong>项目名称</strong>
                                </td>
                                <td>
                                    <strong>合同号</strong>
                                </td>
                                <td visible="false">
                                    <strong>任务单号</strong>
                                </td>
                                <td>
                                    <strong>总序</strong>
                                </td>
                                <td>
                                    <strong>图号</strong>
                                </td>
                                <td>
                                    <strong>产品名称</strong>
                                </td>
                                <td>
                                    <strong>出库数量(扫码填写)</strong>
                                </td>
                                <td>
                                    <strong>已比价数量</strong>
                                </td>
                                <td>
                                    <strong>库存数量</strong>
                                </td>
                                <td>
                                    <strong>备注</strong>
                                </td>
                                <td>
                                    <strong>扫码备注</strong>
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex+1%>
                                    <asp:Label ID="CM_FID" runat="server" Text='<%#Eval("CM_FID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="CM_ID" runat="server" Text='<%#Eval("CM_ID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="KC_SINGNUM" runat="server" Text='<%#Eval("KC_SINGNUMBER")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="CPQROut_ID" runat="server" Text='<%#Eval("CPQROut_ID")%>' Visible="false"></asp:Label>
                                </td>
                                
                                <td>
                                    <asp:Label ID="lbCPQROut_TaskID" runat="server" Text='<%#Eval("CPQROut_TaskID")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCPQROut_Time" runat="server" Text='<%#Eval("CPQROut_Time")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCPQROut_Person" runat="server" Text='<%#Eval("CPQROut_Person")%>'></asp:Label>
                                </td>
                                
                                <td>
                                    <asp:Label ID="CM_BIANHAO" runat="server" Text='<%#Eval("CM_BIANHAO")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblProj" runat="server" Text='<%#Eval("KC_PROJ")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblContr" runat="server" Text='<%#Eval("BM_PJID")%>'></asp:Label>
                                </td>
                                <td visible="false">
                                    <asp:Label ID="lblTsa" runat="server" Text='<%#Eval("BM_ENGID")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="CPQROut_Zongxu" runat="server" Text='<%#Eval("CPQROut_Zongxu")%>'></asp:Label>
                                    <asp:Label ID="KC_ZONGXU" Visible="false" runat="server" Text='<%#Eval("KC_ZONGXU")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMap" runat="server" Text='<%#Eval("BM_TUHAO")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEngname" runat="server" Text='<%#Eval("BM_CHANAME")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCPQROut_Num" onblur="CheckNum(this)" Text='<%#Eval("CPQROut_Num") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="txtYfnum" runat="server" Text='<%#Eval("BM_YFNUM")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblkcnum" runat="server" Text='<%#Eval("KC_KCNUM")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblnote" runat="server" Text='<%#Eval("TSA_IDNOTE")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lbCPQROut_Note" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;text-align: center" Width="150px" Text='<%#Eval("CPQROut_Note")%>' ToolTip='<%#Eval("CPQROut_Note")%>'></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="NoDataPanel" runat="server">
                    没有相关信息!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
</asp:Content>

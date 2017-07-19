<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchange_all_detail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchange_all_detail"
    Title="变更管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="commanrepeater.css" rel="stylesheet" />

    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        function setto(no, obj) {
            var $tr = $("#tab tr:not(:first,:last):not(:last)");
            var pattem = /^(-)?\d+(\.\d+)?$/;
            var a;
            var b;
            var sum = parseFloat(0);
            $.each($tr, function() {
                a = $("input", $("td", $(this)).eq(no)).eq(0).val();
                b = parseFloat($("span", $("td", $(this)).eq(no - 2)).eq(0).text());
                if (pattem.test(a)) {
                    if (Math.abs(b) < Math.abs(a)) {
                        a = b;
                        obj.value = a;
                    }
                    if (a < 0) {
                        obj.value = Math.abs(a);
                    }
                }
                else {
                    a = 0;
                    obj.value = "0";
                }
                sum += parseFloat(Math.abs(a));
            });
            var trs = $("#change tr");
            var change = "";
            if (trs.length > 0) {
                if (no == 10) {
                    //change = trs[2].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
                    change = $("input", $("td", trs.eq(2)).eq(1)).eq(0).val();
                }
                else if (no == 11) {
                    //change = trs[2].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value;
                    change = $("input", $("td", trs.eq(2)).eq(2)).eq(0).val();
                }
                if (sum > Math.abs(change)) {
                    sum = sum - obj.value;
                    obj.value = 0;
                }
            }
            $("input", $("td", $("#tab tr").get($("#tab tr").length - 2)).eq(no - 7)).eq(0).val(sum.toFixed(4));
            //tr[tr.length - 2].getElementsByTagName("td")[no - 7].getElementsByTagName("input")[0].value = sum.toFixed(4);
        }
    </script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:CheckBox runat="server" ID="CheckBox1" Checked="false" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:DropDownList ID="DropDownListBKPersom" runat="server" Visible="false">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_cancel" runat="server" Text="取消" OnClick="btn_cancel_click" Visible="false" />&nbsp;&nbsp;
                                <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_click" Visible="false" />&nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_click" />&nbsp;
                                <%--<a href="javascript:history.go(-1);">向上一页</a> &nbsp;&nbsp;--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div style="overflow: scroll; width: 100%; height: 100%">
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="width: 100%">
                                    <div>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tb_pcode" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="tb_pjid" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="tb_pjname" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="tb_engid" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="tb_engname" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="tb_marid" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="tb_ptcode" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="tb_id" runat="server" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: small; text-align: center;" colspan="4">
                                                    变更信息
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table id="change" width="100%" class="nowrap cptable fullwidth">
                                        <asp:Repeater ID="tbpc_purbgdetailRepeater" runat="server" OnItemDataBound="tbpc_purbgdetailRepeater_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                    <td>
                                                        <strong>行号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>变更批号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>计划跟踪号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>物料编码</strong>
                                                    </td>
                                                    <td>
                                                        <strong>名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>规格</strong>
                                                    </td>
                                                    <td>
                                                        <strong>材质</strong>
                                                    </td>
                                                    <td>
                                                        <strong>国标</strong>
                                                    </td>
                                                    <td>
                                                        <strong>单位</strong>
                                                    </td>
                                                    <td>
                                                        <strong>变更数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>变更辅助数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>执行数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>执行辅助数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>长度</strong>
                                                    </td>
                                                    <td>
                                                        <strong>宽度</strong>
                                                    </td>
                                                    <td>
                                                        <strong>备注</strong>
                                                    </td>
                                                    <td>
                                                        <strong>状态</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                    <td>
                                                        <%--<%#Container.ItemIndex + 1 %>--%>
                                                        &nbsp;
                                                        <asp:Label ID="ROWBGSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_PCODE" runat="server" Text='<%#Eval("BG_PCODE")%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="BG_ID" runat="server" Text='<%#Eval("MP_ID")%>' Visible="false"></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_PTCODE" runat="server" Text='<%#Eval("BG_PTCODE")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_MARID" runat="server" Text='<%#Eval("BG_MARID")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_MARNAME" runat="server" Text='<%#Eval("BG_MARNAME")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_MARNORM" runat="server" Text='<%#Eval("BG_MARNORM")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_MARTERIAL" runat="server" Text='<%#Eval("BG_MARTERIAL")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_GUOBIAO" runat="server" Text='<%#Eval("BG_GUOBIAO")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_NUNIT" runat="server" Text='<%#Eval("BG_NUNIT")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_NUM" runat="server" Text='<%#Eval("BG_NUM")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_FZNUM" runat="server" Text='<%#Eval("BG_FZNUM")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_ZXNUM" runat="server" Text='<%#Eval("BG_ZXNUM")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_ZXFZNUM" runat="server" Text='<%#Eval("BG_ZXFZNUM")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LENGTH" runat="server" Text='<%#Eval("LENGTH")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="WIDTH" runat="server" Text='<%#Eval("WIDTH")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_NOTE" runat="server" Text='<%#Eval("BG_NOTE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BG_STATE" runat="server" Text='<%#Eval("MP_STATE")%>'></asp:Label>
                                                        <asp:Label ID="BG_STATE1" runat="server" Text='<%#Eval("MP_STATE")%>' Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr align="center">
                                                    <td rowspan="1" colspan="9">
                                                        汇总:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_bgnum" runat="server" BorderStyle="None" BackColor="#EFF3FB"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_bgfznum" runat="server" BorderStyle="None" BackColor="#EFF3FB"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_zxnum" runat="server" BorderStyle="None" BackColor="#EFF3FB"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_zxfznum" runat="server" BorderStyle="None" BackColor="#EFF3FB"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <td colspan="4">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 100%">
                                    <div>
                                        <table width="100%">
                                            <tr>
                                                <td style="font-size: small; text-align: center;" colspan="4">
                                                    材料信息
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                                        <asp:Repeater ID="tbpc_purallyjhdetailRepeater" runat="server" OnItemDataBound="tbpc_purallyjhdetailRepeater_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                    <td>
                                                        <strong>行号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>计划号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>材料ID</strong>
                                                    </td>
                                                    <td>
                                                        <strong>名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>规格</strong>
                                                    </td>
                                                    <td>
                                                        <strong>材质</strong>
                                                    </td>
                                                    <td>
                                                        <strong>国标</strong>
                                                    </td>
                                                    <td>
                                                        <strong>单位</strong>
                                                    </td>
                                                    <td>
                                                        <strong>计划数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>辅助数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>变更数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>变更辅助数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>长度</strong>
                                                    </td>
                                                    <td>
                                                        <strong>宽度</strong>
                                                    </td>
                                                    <td>
                                                        <strong>采购员</strong>
                                                    </td>
                                                    <td>
                                                        <strong>是否询比价</strong>
                                                    </td>
                                                    <td>
                                                        <strong>是否下订单</strong>
                                                    </td>
                                                    <td>
                                                        <strong>备注</strong>
                                                    </td>
                                                    <td id="td0" runat="server" visible="false">
                                                        <strong>库存唯一标识符</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                    <td>
                                                        <asp:Label ID="Lab_NUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PTCODE" runat="server" Text='<%#Eval("PTCODE")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="MARID" runat="server" Text='<%#Eval("MARID")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="MARNAME" runat="server" Text='<%#Eval("MARNAME")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="GUIGE" runat="server" Text='<%#Eval("GUIGE")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="CAIZHI" runat="server" Text='<%#Eval("CAIZHI")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="GUOBIAO" runat="server" Text='<%#Eval("GUOBIAO")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="UNIT" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="RPNUM" runat="server" Text='<%#Eval("RPNUM")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="RPFZNUM" runat="server" Text='<%#Eval("RPFZNUM")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="BGNUM" runat="server" Text="0" onclick="javascript:if (this.value==''){ this.value='0.00';};}"
                                                            onblur="setto(10,this)"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="BGFZNUM" runat="server" Text="0" onclick="javascript:if (this.value==''){ this.value='0.00';};}"
                                                            onblur="setto(11,this)"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LENGTH" runat="server" Text='<%#Eval("LENGTH")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="WIDTH" runat="server" Text='<%#Eval("WIDTH")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="CGMAN" runat="server" Text='<%#Eval("CGMAN")%>' Visible="false"></asp:Label>&nbsp;
                                                        <asp:Label ID="CGMANNM" runat="server" Text='<%#Eval("CGMANNM")%>'></asp:Label>&nbsp;
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="BJD" runat="server" Text='<%#get_bjstate(Eval("STATE").ToString())%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="DD" runat="server" Text='<%#get_ddstate(Eval("STATE").ToString())%>'></asp:Label>
                                                        <asp:Label ID="STATE" runat="server" Text='<%#Eval("STATE")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="NOTE" runat="server" Text='<%#Eval("NOTE")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td id="td0" runat="server" visible="false">
                                                        &nbsp;
                                                        <asp:Label ID="STO" runat="server" Text='<%#Eval("STO")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr align="center">
                                                    <td rowspan="1" colspan="8">
                                                        合计:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_jhtotal1" runat="server" BorderStyle="None" Enabled="False" BackColor="#EFF3FB"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_jhtotal2" runat="server" BorderStyle="None" Enabled="False" BackColor="#EFF3FB"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_jhtotal3" runat="server" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_jhtotal4" runat="server" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                                                    </td>
                                                    <td colspan="6">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td colspan="18" align="center">
                                                <asp:Panel ID="NoDataPanex" runat="server" Visible="false">
                                                    没有数据！</asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="ss" runat="server" style="width: 100%">
                                    <div>
                                        <table width="100%">
                                            <tr>
                                                <td style="font-size: small; text-align: center;" colspan="4">
                                                    采购计划
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table width="100%" class="nowrap cptable fullwidth">
                                        <asp:Repeater ID="tbpc_purbgyclRepeater" runat="server" OnItemDataBound="tbpc_purbgyclRepeater_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                    <td>
                                                        <strong>行号</strong>
                                                    </td>
                                                    <td id="hpihao" visible="false">
                                                        <strong>批号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>计划号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>物料编码</strong>
                                                    </td>
                                                    <td id="Td1" runat="server" visible="false">
                                                        <strong>类型</strong>
                                                    </td>
                                                    <td>
                                                        <strong>名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>规格</strong>
                                                    </td>
                                                    <td>
                                                        <strong>材质</strong>
                                                    </td>
                                                    <td>
                                                        <strong>国标</strong>
                                                    </td>
                                                    <td>
                                                        <strong>计划数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>采购数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>单位</strong>
                                                    </td>
                                                    <%-- <td>
                                            <strong>长度</strong>
                                        </td>
                                        <td>
                                            <strong>宽度</strong>
                                        </td>--%>
                                                    <td>
                                                        <strong>采购员</strong>
                                                    </td>
                                                    <td>
                                                        <strong>是否行关闭</strong>
                                                    </td>
                                                    <td>
                                                        <strong>是否询比价</strong>
                                                    </td>
                                                    <td>
                                                        <strong>是否下订单</strong>
                                                    </td>
                                                    <td id="td0" runat="server" visible="false">
                                                        <strong>比价单号</strong>
                                                    </td>
                                                    <td id="td2" runat="server" visible="false">
                                                        <strong>订单号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>状态</strong>
                                                    </td>
                                                    <%--  <td>
                                            <strong>备注</strong>
                                        </td>--%>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                                    ondblclick="databinddbl(this)">
                                                    <td>
                                                        <asp:Label ID="ROWYCLSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                        <%--<asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                AutoPostBack="true" Checked="false" OnCheckedChanged="CheckedChanged"></asp:CheckBox>
                                            &nbsp;--%>
                                                    </td>
                                                    <td id="bpihao" visible="false">
                                                        <asp:Label ID="PUR_PCODE" runat="server" Text='<%#Eval("PUR_PCODE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                    </td>
                                                    <td id="Td3" runat="server" visible="false">
                                                        <asp:Label ID="PUR_MASHAPE" runat="server" Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("PUR_NUM")%>'></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                    </td>
                                                    <%-- <td>
                                            <asp:Label ID="PUR_LENGTH" runat="server" Text='<%#Eval("PUR_LENGTH")%>'></asp:Label>
                                          
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_WIDTH" runat="server" Text='<%#Eval("PUR_WIDTH")%>'></asp:Label>
                                           
                                        </td>--%>
                                                    <td>
                                                        <asp:Label ID="PUR_CGMAN" runat="server" Text='<%#Eval("PUR_CGMAN")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="PUR_CGMANNM" runat="server" Text='<%#Eval("PUR_CGMANNM")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="Hyhclose" runat="server" CssClass="hand">
                                                            <asp:Label ID="PUR_CSTATE1" runat="server" Text='<%#get_pur_cst(Eval("PUR_CSTATE").ToString())%>'></asp:Label></asp:HyperLink>
                                                        <asp:Label ID="PUR_CSTATE" runat="server" Text='<%#Eval("PUR_CSTATE")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="Hypbjd" runat="server" CssClass="hand">
                                                            <asp:Label ID="PUR_BJD" runat="server" Text='<%#get_pur_bjd(Eval("picno").ToString())%>'></asp:Label></asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="Hyporder" runat="server" CssClass="hand">
                                                            <asp:Label ID="PUR_ORDER" runat="server" Text='<%#get_pur_dd(Eval("orderno").ToString())%>'></asp:Label></asp:HyperLink>
                                                    </td>
                                                    <td id="td0" runat="server" visible="false">
                                                        <asp:Label ID="PIC_SHEETNO" runat="server" Text='<%#Eval("picno")%>'></asp:Label>
                                                    </td>
                                                    <td id="td1" runat="server" visible="false">
                                                        <asp:Label ID="PO_SHEETNO" runat="server" Text='<%#Eval("orderno")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("PUR_STATE")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="PUR_STATETEXT" runat="server" Text='<%#get_pur_state(Eval("PUR_STATE").ToString())%>'></asp:Label>
                                                    </td>
                                                    <%-- <td>
                                            <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                        </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td colspan="19" align="center">
                                                <asp:Panel ID="NoDataPanep" runat="server" Visible="false">
                                                    没有数据！</asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

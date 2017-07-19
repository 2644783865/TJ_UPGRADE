<%@ Page Language="C#" MasterPageFile="~/Masters/auditMaster.Master" AutoEventWireup="true"
    CodeBehind="jiPiaodetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.jiPiaodetail" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function checkplandate(obj){
            var startplandate=obj.parentNode.parentNode.getElementsByTagName("td")[5].getElementsByTagName("input")[0].value;
            var endplandate=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
            if(endplandate<startplandate)
            {
                alert("回程时间不能小于出发时间！");
                obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value="";
            }
            
        }
        function checkrealdate(obj){
            var startrealdate=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            var endrealdate=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            if(endrealdate<startrealdate)
            {
                alert("回程时间不能小于出发时间！");
                obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="";
            }
        }
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }
        
        function rbliffankui(){
            if($("#ctl00_audittabcontainer_tabdetaildata_ContentPlaceHolder1_iffankui").attr("checked"))
            {
                $("#trfankui").show();
            }
            else
            {
                $("#trfankui").hide();
            }
        }
        
        $(function(){
            if($("#ctl00_audittabcontainer_tabdetaildata_ContentPlaceHolder1_iffankui").attr("checked"))
            {
                $("#trfankui").show();
            }
            else
            {
                $("#trfankui").hide();
            }
        });
    </script>

    <div style="width: 100%">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Label ID="audittitle" runat="server" Text="机票申请"></asp:Label><%--审批类型，每次添加新的审批时，必须修改--%>
                </td>
            </tr>
            <tr>
                <td id="td1" align="right">
                    <input type="checkbox" onclick="rbliffankui()" id="iffankui" runat="server" /><label for="iffankui">&nbsp;是否需要制单人反馈</label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="btnsave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
            border="1">
            <tr>
                <td align="left">
                        &nbsp;&nbsp;&nbsp;行数：
                        <asp:TextBox ID="txtNum" runat="server" Width="50px" onblur="CheckNum(this);"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnadd" runat="server" Text="确 定" OnClick="btnadd_Click" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    单号：
                </td>
                <td align="center">
                    <asp:Label ID="txt_jipiaocontentno" runat="server"></asp:Label>
                </td>
                <td align="center">
                    部门
                </td>
                <td align="center">
                    <asp:TextBox ID="txt_dep" runat="server" Enabled="false"></asp:TextBox>
                    <asp:HiddenField ID="hiddepid" runat="server" />
                    <asp:HiddenField ID="hidstate" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table width="100%">
            <tr>
                <td align="center" colspan="7">
                    <asp:Label ID="Label1" runat="server" Text="数据明细"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tab" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <asp:Repeater ID="rpt1" runat="server">
                <HeaderTemplate>
                    <tr align="center" style="background-color: #B9D3EE; text-overflow: ellipsis; white-space: nowrap;">
                        <td>
                            序号
                        </td>
                        <td>
                            姓名
                        </td>
                        <td>
                            出发地
                        </td>
                        <td>
                            目的地
                        </td>
                        <td>
                            费用
                        </td>
                        <td>
                            计划出发日期
                        </td>
                        <td>
                            计划回程日期
                        </td>
                        <td>
                            实际出发日期
                        </td>
                        <td>
                            实际回程日期
                        </td>
                        <td>
                            备注
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id="row" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                        onclick="javascript:change(this);" ondblclick="javascript:changeback(this);"
                        style="text-overflow: ellipsis; white-space: nowrap;">
                        <td>
                            <asp:Label ID="detailID" runat="server" Text='<%#Eval("detailID")%>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="chk" CssClass="checkBoxCss" runat="server" Checked="false"
                                Onclick="checkme(this)" />
                            <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                        </td>
                        <td align="center">
                            <asp:TextBox ID="detailpername" OnTextChanged="Textname_TextChanged" AutoPostBack="true"
                                onclick="this.select();" runat="server" Text='<%#Eval("detailpername")%>' Width="80px"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                                ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="detailpername"
                                UseContextKey="True">
                            </asp:AutoCompleteExtender>
                            <asp:Label ID="detailperid" runat="server" Text='<%#Eval("detailperid")%>' Visible="false"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="startlocation" runat="server" Text='<%#Eval("startlocation")%>' Width="80px"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="endlocation" runat="server" Text='<%#Eval("endlocation")%>' Width="80px"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="money" runat="server" align="center" BorderStyle="None"
                                BackColor="Transparent" Text='<%#Eval("money")%>' Width="80px"></asp:TextBox>
                        </td>
                        <td align="center">
                            <input type="text" id="planstartdate" value='<%#Eval("planstartdate")%>' style="width:100px" onfocus="this.blur()" runat="server" class="easyui-datebox" />
                        </td>
                        <td align="center">
                            <input type="text" id="planenddate" value='<%#Eval("planenddate")%>' style="width:100px" onfocus="this.blur()" runat="server" class="easyui-datebox" />
                        </td>
                        <td align="center">
                            <input type="text" id="realstartdate" value='<%#Eval("realstartdate")%>' style="width:100px" onfocus="this.blur()" runat="server" class="easyui-datebox" />
                        </td>
                        <td align="center">
                            <input type="text" id="realenddate" value='<%#Eval("realenddate")%>' style="width:100px" onfocus="this.blur()" runat="server" class="easyui-datebox" />
                        </td>
                        <td align="center">
                            <asp:TextBox ID="detailnote" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                Text='<%#Eval("detailnote")%>' Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div style="float: left; padding-top: 10px">
        &nbsp; &nbsp; &nbsp;<asp:Button ID="delete" runat="server" Text="删除行" OnClick="delete_Click" /></div>
    <div>
        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
            border="1">
            <tr id="trfankui">
                <td align="center">
                    制单人反馈
                </td>
                <td colspan="3">
                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                        <tr>
                            <td align="center" style="width: 10%">
                                反馈时间
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lbfankuitime" runat="server" Width="100%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txtfaikuinote" runat="server" TextMode="MultiLine" Width="100%"
                                    Height="42px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

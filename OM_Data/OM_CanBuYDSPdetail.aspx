<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_CanBuYDSPdetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CanBuYDSPdetail"
    Title="餐补异动审批" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    餐补异动审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    
        var i;
        array = new Array();
        function SelPersons1() {
            $("#hidPerson").val("first");
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
            $('#win').dialog('close');
        }
    </script>

    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }

        function GetBF(obj){ 
        
          if(isNaN(parseFloat(obj.value)))
           {
           alert("请输入正确的数量!");
           obj.value=0;
           return; 
           }
//          obj.value=parseInt(tzts)*(cbbz);
           var tr=obj.parentNode.parentNode;
           var obj_tzts=tr.getElementsByTagName("td")[4].getElementsByTagName("input")[0];
           var obj_cbbz=tr.getElementsByTagName("td")[5].getElementsByTagName("input")[0];

           if(obj_tzts.value!=""&&obj_cbbz.value!="")
           {  
             tr.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=(obj_tzts.value)*(obj_cbbz.value);
           }
          }

    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <table style="width: 100%">
            <tr>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;行数：
                    <asp:TextBox ID="txtNum" runat="server" Width="50px" onblur="CheckNum(this);"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnadd" runat="server" Text="确 定" OnClick="btnadd_Click" Visible="false" />
                </td>
                <td align="right">
                    年月：
                    <asp:TextBox ID="tb_yearmonth" Width="90px" runat="server" Enabled="false"></asp:TextBox>
                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                        ID="CalendarExtender1" runat="server" Format="yyyy-MM" TargetControlID="tb_yearmonth">
                    </asp:CalendarExtender>
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_OnClick" Visible="false" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnSubmit" Text="提交" OnClick="btnSubmit_OnClick" Visible="false" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnfanshen" Text="反审" OnClick="btnfanshen_OnClick"
                        Visible="false" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" ActiveTabIndex="0">
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="餐补异动明细" TabIndex="0">
            <ContentTemplate>
                <div style="overflow: scroll; height: 500px;">
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Button runat="server" ID="btndelete" Text="删除" OnClick="btndelete_OnClick" Visible="false" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table id="table1" align="center" cellpadding="2" cellspacing="1" border="1">
                        <tr align="center">
                            <td align="center" colspan="10" style="border: none">
                                餐补异动<asp:Label ID="lb_title" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td align="left" colspan="10" style="border: none">
                                制单人：<asp:Label ID="lbtitle_zdr" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单时间：<asp:Label
                                    ID="lbtitle_zdsj" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <asp:Repeater ID="rptProNumCost" runat="server">
                            <HeaderTemplate>
                                <tr align="center" style="background-color: #B9D3EE; text-overflow: ellipsis; white-space: nowrap;">
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        姓名
                                    </td>
                                    <td>
                                        工号
                                    </td>
                                    <td>
                                        部门
                                    </td>
                                    <td>
                                        调整天数
                                    </td>
                                    <td>
                                        餐补标准
                                    </td>
                                    <td>
                                        补发
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
                                        <asp:Label ID="lbCBYD_ID" runat="server" Text='<%#Eval("CBYD_ID")%>' Visible="false"></asp:Label>
                                        <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                            Onclick="checkme(this)" />
                                        <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="lbST_NAME" OnTextChanged="Textname_TextChanged" AutoPostBack="true"
                                            onclick="this.select();" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                                            ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="lbST_NAME"
                                            UseContextKey="True">
                                        </asp:AutoCompleteExtender>
                                        <asp:Label ID="lbCBYD_STID" runat="server" Text='<%#Eval("CBYD_STID")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtST_WORKNO" runat="server" Text='<%#Eval("ST_WORKNO")%>'></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtDEP_NAME" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtCBYD_TZTS" runat="server" align="center" BorderStyle="None" onkeyup="GetBF(this);"
                                            BackColor="Transparent" Text='<%#Eval("CBYD_TZTS")%>' Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtCBYD_CBBZ" runat="server" align="center" BorderStyle="None" onkeyup="GetBF(this);"
                                            BackColor="Transparent" Text='<%#Eval("CBYD_CBBZ")%>' Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtCBYD_BF" runat="server" align="center" BorderStyle="None"
                                            BackColor="Transparent" Text='<%#Eval("CBYD_BF")%>' Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtCBYD_Note" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("CBYD_Note")%>' Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" TabIndex="2" Width="100%" HeaderText="审核">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                    餐补异动
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
                                <td align="center">
                                    制单人
                                </td>
                                <td>
                                    <asp:Label ID="lbzdr" runat="server" Width="100%"></asp:Label>
                                </td>
                                <td align="center">
                                    制单时间
                                </td>
                                <td>
                                    <asp:Label ID="lbzdtime" runat="server" Width="40%" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    制单人意见
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtfqryj" runat="server" Height="42px" TextMode="MultiLine" Width="100%"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="yjshh" runat="server">
                                <td align="center">
                                    一级审核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" Visible="false" runat="server" CssClass="hand" onClick="SelPersons1()">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" Enabled="false" RepeatColumns="2" runat="server"
                                                    Height="20px">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
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
                                                <asp:TextBox ID="opinion1" runat="server" Enabled="false" Height="42px" TextMode="MultiLine"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>

    </asp:TabContainer>
        <table width="100%">
           <tr>
                <td>
                  合计金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>
                 </td>
           </tr>
       </table>
    <div>
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
                                <input id="dep" name="dept" />
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
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>
</asp:Content>

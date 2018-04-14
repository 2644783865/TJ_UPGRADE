<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_ExpressDetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_ExpressDetail1"
    Title="快递管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    快递管理 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
     function checkphone(obj){
//       var myreg = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$/; 
//       if((!myreg.test($(obj).val()))||obj.length!=11){ 
//          alert('请输入有效的手机号码！'); 
//              return false; 
//         } 
      }
      function valid(){
        if(($('#<%=txt_E_SQR.ClientID%>').val()=="")||($('#<%=txt_E_SendTo.ClientID%>').val()=="")||($('#<%=txt_E_Address.ClientID%>').val()=="")){
        alert('申请人姓名、收件单位及邮寄地址为必填项');
          return false;
        }
        return true;
      }
      function  surevalid(){
        if(($('#<%=ckb_E_BackRefuse.ClientID%>').attr("checked")==false)&&(($('#<%=txt_E_ExpressMoney.ClientID%>').val()=="")||($('#<%=ddl_E_ExpressCompany.ClientID%>').val()==""))){
        alert('请填写快递公司和快递费用');
          return false;
        }
        return true;
      }
      
      $(function(){
      //    var curr_time = new Date();
      //    var strDate = curr_time.getFullYear()+"-"+ curr_time.getMonth()+1+"-"+curr_time.getDate();
      //    $('#<%=txt_E_ExpressTime.ClientID%>').datebox("setValue", strDate); 
        $('#<%=txt_E_ExpressMoney.ClientID%>').keyup(function(e) {
           var regex = /^\d+(\.\d{0,2})?$/g;
           if (!regex.test(this.value)) {
           alert('请输入小数位数最多2位的数字');
           this.value = '';
            }
          });                
      })
    </script>

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
            width: 250px;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
            width: 250px;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
            width: 250px;
        }
        .center
        {
            text-align: center;
        }
    </style>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnSave" runat="server" Text="保 存" OnClientClick="return valid();"
                                    OnClick="btnSave_OnClick" />
                                <asp:Button ID="btnSubmit" runat="server" Text="提 交" Visible="false" OnClick="btnSubmit_OnClick"
                                    CssClass="button-outer" OnClientClick="return confirm('如需修改请返回快递管理页面进行编辑，提交审批后不可修改。\r\n确定提交审批？')" /><asp:Button
                                        ID="btnSure" runat="server" Text="确 认" Visible="false" OnClientClick="return surevalid()"
                                        OnClick="btnSure_OnClick" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_OnClick"
                                    CausesValidation="False" CssClass="button-outer" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="update_body" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                    ActiveTabIndex="1">
                    <cc1:TabPanel ID="TabDetail" runat="server" HeaderText="申请内容" TabIndex="0" Width="100%">
                        <ContentTemplate>
                            <asp:Panel ID="PanelDetail" runat="server">
                                <div align="center" style="font-size: 20px; color: Red">
                                    <strong>快递申请单</strong>
                                </div>
                                <br />
                                <table width="100%" align="center">
                                    <tr align="center">
                                        <td style="width: 21%">
                                            申请单号:
                                            <asp:Label ID="lb_E_Code" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 16%">
                                            制单人:
                                            <asp:Label ID="lb_E_ZDR" runat="server"></asp:Label>
                                            <asp:Label ID="lb_E_ZDRID" Visible="false" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 16%">
                                            制单部门:
                                            <asp:Label ID="lb_E_SQRDep" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 27%">
                                            制单时间:
                                            <asp:Label ID="lb_E_ZDTime" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Panel ID="PanelExpress" runat="server">
                                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                                        width="100%">
                                        <tr>
                                            <td width="80px" rowspan="4" align="center">
                                                申请内容
                                            </td>
                                        </tr>
                                        <tr style="height: 25px;">
                                            <td width="80px" align="center">
                                                申请人
                                            </td>
                                            <td align="center" width="20%">
                                                <asp:TextBox ID="txt_E_SQR" Height="17px" CssClass="center" AutoPostBack="true" runat="server"
                                                    OnTextChanged="SQRName_TextChanged"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                                    DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id_new"
                                                    ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txt_E_SQR"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                </cc1:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写申请人"
                                                    ControlToValidate="txt_E_SQR" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txt_E_SQRID" Visible="false" runat="server"></asp:TextBox>
                                            </td>
                                            <td width="80px" align="center">
                                                邮寄类别
                                            </td>
                                            <td width="50px">
                                                <asp:DropDownList ID="ddl_E_Type" Height="17px" runat="server" OnSelectedIndexChanged="ddl_E_Type_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="文件类" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="物品类" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="80px" id="td_wenjian0" runat="server" align="center">
                                                邮寄文件内容
                                            </td>
                                            <td colspan="3" id="td_wenjian1" runat="server">
                                                <asp:TextBox ID="txt_E_FileName" placeholder="填写文件名称（用途）" TextMode="MultiLine" Width="98.5%"
                                                    Height="21px" runat="server"></asp:TextBox>
                                            </td>
                                            <td width="80px" id="td_wuping0" runat="server" visible="false" align="center">
                                                邮寄物品名称
                                            </td>
                                            <td id="td_wuping1" runat="server" visible="false" align="center">
                                                <asp:TextBox ID="txt_E_ItemName" placeholder="填写物品名称、合同号" TextMode="MultiLine" Width="95%"
                                                    Height="21px" runat="server"></asp:TextBox>
                                            </td>
                                            <td width="80px" id="td_wuping2" runat="server" visible="false">
                                                邮寄物品重量
                                            </td>
                                            <td id="td_wuping3" runat="server" visible="false">
                                                <asp:TextBox ID="txt_E_ItemWeight" CssClass="center" TextMode="MultiLine" Width="96%"
                                                    Height="21px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80px" align="center">
                                                收件单位
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txt_E_SendTo" runat="server" TextMode="MultiLine" Width="98%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写收件单位"
                                                    ControlToValidate="txt_E_SendTo" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td width="80px" align="center">
                                                邮寄地址
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txt_E_Address" runat="server" Width="98.5%" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请填写邮寄地址"
                                                    ControlToValidate="txt_E_Address" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80px" align="center">
                                                特殊说明
                                            </td>
                                            <td colspan="7" align="center">
                                                <asp:TextBox ID="txt_E_Note" TextMode="MultiLine" placeholder="提示：1、邮寄物品的重要程度；2、是否为紧急件；3、其他（任务号等）"
                                                    runat="server" Width="99%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="PanelExpressBack" runat="server">
                                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                        <tr>
                                            <td width="10%" rowspan="2">
                                                执行情况反馈(<asp:Label ID="lb_E_Surer" runat="server"></asp:Label><asp:HiddenField ID="hid_E_SurerID"
                                                    runat="server" />
                                                )
                                            </td>
                                            <td width="10%" align="center">
                                                快递公司
                                            </td>
                                            <td width="10%" align="center">
                                                快递单号
                                            </td>
                                            <td width="10%" align="center">
                                                快递重量
                                            </td>
                                            <td width="10%" align="center">
                                                快递费用
                                            </td>
                                            <td width="10%" align="center">
                                                发出时间
                                            </td>
                                            <td align="center">
                                                <asp:CheckBox ID="ckb_E_BackRefuse" name="ckb_E_BackRefuse" runat="server" Text="驳回"
                                                    ForeColor="Red" />&nbsp;&nbsp;&nbsp;&nbsp;备注
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:DropDownList runat="server" ID="ddl_E_ExpressCompany" AutoPostBack="false">
                                                    <asp:ListItem Text="请选择" Value="" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="百世汇通" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="顺丰" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="邮政EMS" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="物流" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="其他" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="自行联系" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txt_E_ExpressCode" CssClass="center" runat="server" Height="17px"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txt_E_ExpressWeight" CssClass="center" runat="server" Height="17px"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txt_E_ExpressMoney" CssClass="center" placeholder="快递费用保留2位小数" runat="server"
                                                    Height="17px"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txt_E_ExpressTime"  runat="server" class="easyui-datebox"
                                                    Height="17px"></asp:TextBox>
                                            </td>
                                            <td style="width: 96%">
                                                <asp:TextBox ID="txt_E_BackNote" CssClass="center" runat="server" TextMode="MultiLine"
                                                    Width="99%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabShenhe" runat="server" HeaderText="审批信息" TabIndex="1" Width="100%">
                        <ContentTemplate>
                            <asp:Panel ID="PanelShenhe" runat="server">
                                <div class="box-wrapper">
                                    <div class="box-outer">
                                        <table width="100%">
                                            <tr>
                                                <td style="font-size: large; text-align: center; height: 43px" colspan="2">
                                                    快递申请审批
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="box-outer">
                                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                            border="1">
                                            <tr>
                                                <td align="center">
                                                    部门领导
                                                </td>
                                                <td colspan="3">
                                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                                        <tr style="height: 25px">
                                                            <td style="width: 20%" align="center">
                                                                <asp:TextBox ID="txt_E_SHR" runat="server" onfocus="this.blur()" Width="80px"></asp:TextBox>
                                                                <asp:TextBox ID="txt_E_SHRID" runat="server" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td align="center" style="width: 10%">
                                                                审核结论
                                                            </td>
                                                            <td align="center" style="width: 20%">
                                                                <asp:RadioButtonList ID="rbl_E_SHResult" RepeatColumns="2" runat="server" Height="20px">
                                                                    <asp:ListItem Text="同意" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td align="center" style="width: 10%">
                                                                审核时间
                                                            </td>
                                                            <td style="width: 20%">
                                                                <asp:Label ID="lb_E_SHTime" runat="server" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                <asp:TextBox ID="txt_E_SHNote" runat="server" TextMode="MultiLine" Width="100%" Height="42px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

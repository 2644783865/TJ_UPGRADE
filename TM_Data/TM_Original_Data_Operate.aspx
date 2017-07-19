<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Original_Data_Operate.aspx.cs"
    Inherits="ZCZJ_DPF.TM_Data.TM_Original_Data_Operate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>原始数据修改</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />
    <JSR:JSRegister ID="JSRegister1" runat="server" />

    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/TMOrgInput.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
        function window.confirm(str1, str2) {
            execScript("n   =   msgbox('" + str1 + "'&vbCrlf&vbCrlf+'提示:'&vbCrlf&vbCrlf+'" + str2 + "',273,'信息提示')", "vbscript");
            if (+n == 1) {
                return true;
            }
            else if (+n == 2) {
                return false;
            }
        }

        function SubmitConfirm(obj) {
            if (obj.value == "修改") {
                return window.confirm("确认修改吗？", "下级也将修改,如：3.2>3.3，则3.2.1>3.3.1");
            }
        }



        function showtip() {
            window.showModalDialog("TM_Original_Data_Operate_sptip.aspx", '', "dialogHeight:200px;dialogWidth:500px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
        }

        function SubmitData(obj) {
           
            if (CheckNumNotZeroWithPurUnit(2)) {
              obj.disabled = true;
//                __doPostBack("btnConfirm", "");
          }
       
        }

        function CalTuUnitRealWgt(obj) {
            var txt = obj.value;
            var pattem = /^[0-9]+(\.[0-9]+)?$/;
            if (pattem.test(txt)) {
                var shuling = document.getElementById("shuliang").value;
                document.getElementById("txtTuZzXuhao").value = (parseFloat(txt) * shuling).toFixed(2);
            }
            else {
                alert("请输入正确的数值格式！！！");
                obj.value = "0";
            }
        }

        function CalWeight(obj) {
            obj.disabled = true;
            document.getElementById("btnSigCalWeight").disabled = true;
            document.getElementById("btnConfirmReal").disabled = true;
            __doPostBack("btnCalWeightHidden", "");
        }

        function SigCalWeight(obj) {
            obj.disabled = true;
            document.getElementById("btnCalWeight").disabled = true;
            document.getElementById("btnConfirmReal").disabled = true;
            __doPostBack("btnSigCalWeightHidden", "");
        }

        function getClientId() {
            var paraId5 = '<%= txtBxishu.ClientID %>';
            var paraId6 = '<%= txtXxishu.ClientID %>';
            return { BXishu: paraId5, XXishu: paraId6 }; //生成访问器
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" style="width: 900px;">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-wrapper">
                <div class="box-outer">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblXishu" runat="server" Text="" ForeColor="Red"
                                    Font-Bold="true"></asp:Label>
                            </td>
                            <td align="right">
                                <strong>计划系数:</strong>(板)<input id="txtBxishu" runat="server" type="text" onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.1";this.foucs();}'
                                    style="width: 30px" value="1.1" />(型/圆钢)<input id="txtXxishu" runat="server" style="width: 30px"
                                        onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.05";this.foucs();}'
                                        type="text" value="1.05" />(其它)<input id="txtQxishu" runat="server" style="width: 30px"
                                            disabled="disabled" type="text" value="1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <tr>
                            <td class="tdleft1">
                                物料编码:
                            </td>
                            <td class="tdright1" colspan="3" >
                                <asp:TextBox ID="marid" runat="server" onchange="modifyCode()"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionSetCount="10" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1"
                                    ServiceMethod="HmCode" ServicePath="~/Ajax.asmx" TargetControlID="marid" UseContextKey="True">
                                </cc1:AutoCompleteExtender>
                                <font color="#ff0000">*</font>
                                <asp:Button ID="btnAddMarID" runat="server" OnClick="btnAddMarID_OnClick" Text="添加物料编码" />
                                &nbsp;
                                <input id="hdmarid" type="text" runat="server" readonly="readonly" style="display: none" />
                                &nbsp;&nbsp;
                                <asp:Label ID="lblTip" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"
                                    Text="审核中或驳回,数据无法修改..."></asp:Label>
                                &nbsp;&nbsp;
                                <asp:Label ID="lblAlt" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"
                                    Text="【代用】"></asp:Label>
                                    </td>
                             
                          
                        </tr>
                        <tr>
                        <td class="tdleft1">
                                名称:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cnname" runat="server" ></asp:TextBox>
                                <input id="hdcnname" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>

                            <td class="tdleft1">
                                图号:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="tuhao" runat="server"></asp:TextBox>
                                <input id="hdtuhao" type="text" runat="server"  style="display: none" /><font
                                    color="#ff0000">*</font>
                            </td>
                        </tr>
                        <tr>
                                                    <td class="tdleft1">
                                总序:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="zongxu" runat="server" Enabled="false"></asp:TextBox>&nbsp;&nbsp;
                                <div>
                                    <asp:Button ID="btnChangeZX" runat="server" Text="修改" OnClientClick="return SubmitConfirm(this);"
                                        CommandName="ActivateIndex" OnClick="btnChangeZX_OnClick" />
                                    <input id="hdzongxu" type="text" runat="server" readonly="readonly" style="display: none" />&nbsp;&nbsp;&nbsp;<span
                                        runat="server" id="div_2" style="color: Red;"><asp:CheckBox ID="ckbWchgZX" runat="server"
                                            Checked="true" />总序下级也修改</span>
                                </div>
                            </td>
                             <td class="tdleft1">
                                单台数量|台数:<br />
                                总数量|计划数量:
                            </td>
                            <td class="tdright1">
                                <input id="sing_shuliang" type="text" runat="server" onchange="modifyCalculation(3)" 
                                    title="单台数量" style="width: 50px;" /><font color="#ff0000">*</font>
                                <input id="hdshuliang" type="text" runat="server" onfocus="this.blur();" readonly="readonly"
                                    style="display: none; width: 50px;" />
                                |&nbsp;&nbsp;&nbsp;<input id="taishu" type="text" runat="server" title="台数" style="width: 50px;
                                    background-color: #EEEEEE" /><br />
                                <asp:TextBox ID="shuliang" runat="server" onfocus="this.blur();" BackColor="#EEEEEE"
                                    title="总数量" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp; |&nbsp;&nbsp;&nbsp;<asp:TextBox
                                        ID="p_shuliang" runat="server" ToolTip="计划数量" onchange="ModifyChangePnum(this);"
                                        Width="50px"></asp:TextBox><font color="#ff0000">*</font><input id="hd_p_shuliang"
                                            type="text" runat="server" readonly="readonly" style="display: none; width: 50px;" />
                            </td>
                            
                           
                        </tr>
                        <tr>
                            <td class="tdleft1">
                                材料长度(mm):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaocd" runat="server" onchange="modifyCalculation(1)"></asp:TextBox><font
                                    color="#ff0000">*</font>&nbsp;&nbsp;<input id="ckbYL" checked="checked" style="display: none;"
                                        title="勾选后增加余量" type="checkbox" />
                                <input id="hdcailiaocd" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                            <td class="tdleft1">
                                材料宽度(mm):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaokd" runat="server" onchange="modifyCalculation(2)"></asp:TextBox><font
                                    color="#ff0000">*</font>
                                <input id="hdcailiaokd" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>
                        <tr>
                        
                          <td class="tdleft1">
                                下料备注:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="xlNote" runat="server" onchange="modifyCalculation(2)"></asp:TextBox><font
                                    color="#ff0000">*</font>
                                <input id="hdxlNote" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                           
                            <td class="tdleft1">
                                材料总长(mm):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaozongchang" runat="server" onchange="Calculation(4);"></asp:TextBox>
                                <input id="hdcailiaozongchang" type="text" runat="server" readonly="readonly" style="display: none" /><font
                                    color="#ff0000">*</font>
                            </td>
                        </tr>
                         <tr>
                           <td class="tdleft1">
                                材料类别:
                            </td>
                            <td class="tdright1">
                               
                                <asp:DropDownList ID="cailiaoType" runat="server">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="板" Value="板"></asp:ListItem>
                                 <asp:ListItem Text="型" Value="型"></asp:ListItem>
                                  <asp:ListItem Text="圆" Value="圆"></asp:ListItem>
                                   <asp:ListItem Text="采" Value="采"></asp:ListItem>
                                 <asp:ListItem Text="锻" Value="锻"></asp:ListItem>
                                  <asp:ListItem Text="铸" Value="铸"></asp:ListItem>
                                   <asp:ListItem Text="采购成品" Value="采购成品"></asp:ListItem>
                                    <asp:ListItem Text="非金属" Value="非金属"></asp:ListItem>   
                                </asp:DropDownList>
                                
                                &nbsp;&nbsp;&nbsp;
                                <input id="hdcailiaoType" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                          
                            <td class="tdleft1">
                                面域|计划面域(m <sup>2</sup>):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="my" runat="server" Width="70px" onchange="Calculation(3)"></asp:TextBox>
                                |&nbsp;<asp:TextBox ID="bgzmy" runat="server" Width="70px" onblur='var pattem=/^\d+(\.\d+)?$/; if(this.value!=""){ if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="0";this.select();}}'></asp:TextBox><font
                                    color="#ff0000">*</font>
                                <input id="hdbgzmy" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>
                        
                        <tr>
                             <td class="tdleft1">
                                图纸上单重(kg):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="txtTuDz" onchange="CalTuUnitRealWgt(this);" runat="server"></asp:TextBox>
                               
                               <input id="hidTudz" type="hidden" runat="server" readonly="readonly" style="display: none" />
                            </td>
                            <td class="tdleft1">
                                图纸上总重(kg):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="txtTuZz" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                       <td class="tdleft1">
                                单位:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="techUnit" runat="server" Enabled="false" ToolTip="(技术单位)-(采购单位)"></asp:TextBox>
                                <input id="hidtechunit" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                            <td class="tdleft1">材料用量:</td><td class="tdright1"><asp:TextBox ID="txtYongliang" runat="server" ></asp:TextBox>
                                <input id="hidYongliang" type="text" runat="server" readonly="readonly" style="display: none" /></td>
                        </tr>
                         <tr>
                            <td class="tdleft1">
                                材料单重(kg):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaodzh" runat="server" onchange="Calculation(1)"></asp:TextBox><font
                                    color="#ff0000">*</font>
                                <input id="hdcailiaodzh" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                            <td class="tdleft1">
                                材料总重(kg):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaozongzhong" onchange="ChangeMarWeight(this);" runat="server"></asp:TextBox><font
                                    color="#ff0000">*</font>
                                <input id="hdcailiaozongzhong" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdleft1">
                                理论重量(kg):
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="lilunzhl" runat="server" Enabled="false" ToolTip="更改物料时自动替换,无法手动修改"></asp:TextBox>
                                <input id="hdlilunzhl" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                            
                             <td class="tdleft1">
                                国标:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="biaozhun" runat="server" Enabled="false" ToolTip="更改物料时自动替换,无法手动修改"></asp:TextBox>
                                <input id="hdbiaozhun" type="text" runat="server" readonly="readonly" title="更改物料时自动替换,无法手动修改"
                                    style="display: none" />
                            </td>
                            
                            
                        </tr>
                       
                        <tr>
                         <td class="tdleft1">
                                材料规格:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="cailiaoguige" runat="server" Enabled="false" ToolTip="更改物料时自动替换,手动修改无效"></asp:TextBox>
                                <input id="hdcailiaoguige" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        
                            <td class="tdleft1">
                                材质:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="caizhi" runat="server" Enabled="false" ToolTip="更改物料时自动替换,无法手动修改"></asp:TextBox>
                                <input id="hdcaizhi" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                          
                        </tr>
                          <tr>
                         <td class="tdleft1">
                                下料方式:
                            </td>
                            <td class="tdright1">
                                 <asp:TextBox ID="xialiao" runat="server"></asp:TextBox>
                                <input id="hdxialiao" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        
                            <td class="tdleft1">
                                工艺流程:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="txtProcess" runat="server"></asp:TextBox>
                                <input id="hdtxtProcess" type="text" runat="server"  style="display: none" />
                            </td>
                          
                        </tr>
                        <tr>
                           
                             <td class="tdleft1">
                                是否定尺:
                            </td>
                            <td class="tdright1">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rblSFDC" runat="server" RepeatColumns="2" onclick="ModifyFix(this);"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <font color="#ff0000">*</font>
                                        </td>
                                    </tr>
                                </table>
                                <input id="hdtxtSFDC" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                           
                             <td class="tdleft1">
                                图纸上材质:
                            </td>
                            <td class="tdright1">
                                <asp:TextBox ID="txtTZCZ" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                           
                         
                        </tr>
                              <tr>
                            <td class="tdleft1">
                                图纸上问题:
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTZWT" runat="server" TextMode="MultiLine" Width="70%"></asp:TextBox>
                                <input id="Text1" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>
                
                        <tr>
                            <td class="tdleft1" rowspan="2">
                                <asp:Label ID="lblMS" runat="server" Text="制作明细:"></asp:Label>
                            </td>
                            <td class="tdright1" colspan="3">
                                <table width="90%">
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:RadioButtonList ID="rblMSSTA" runat="server" Enabled="false" RepeatColumns="2"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 30%">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblInMS" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="体现" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Text="不体现" Value="N"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <font color="#ff0000">*</font>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <input id="txthdinms" type="text" runat="server" readonly="readonly" style="display: none" />
                                        <td style="width: 50%">
                                            <asp:RadioButtonList ID="rblMSRew" Enabled="false" runat="server" RepeatColumns="4"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="通过" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="驳回" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rblMSChangeSta" runat="server" Enabled="false" RepeatColumns="4"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="正常" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="删除" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="修改" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="增加" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMSRew" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        
                        </tr>
                        <tr>
                            <td class="tdleft1" rowspan="2">
                                <asp:Label ID="lblMP" runat="server" Text="材料计划:"></asp:Label>
                            </td>
                            <td class="tdright1" colspan="3">
                                <table width="90%">
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:RadioButtonList ID="rblMPSTA" runat="server" ToolTip="在能提交材料计划的情况下，材料计划是否提交"
                                                Enabled="false" RepeatColumns="2" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:DropDownList ID="ddlWMarPlan" ToolTip="该条记录是否能提交材料计划" runat="server">
                                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:CheckBox ID="ckbWMarplanToLower" runat="server" />关联到下级
                                        </td>
                                       
                                        <td style="width: 40%">
                                            <asp:RadioButtonList ID="rblMPRew" runat="server" Enabled="false" RepeatColumns="4"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="通过" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="驳回" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                      
                                        <td colspan="2">
                                        
                                        
                                        <table width="100%" >
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rblMPChangSta" runat="server" Enabled="false" RepeatColumns="4"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="正常" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="删除" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="修改" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="增加" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPRew" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                        
                                          
                        </tr>
  
                  

                        <tr>
                            <td class="tdleft1">
                                备注:
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="beizhu" runat="server" TextMode="MultiLine" Height="50px" Width="70%"></asp:TextBox>
                                <input id="hdbeizhu" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>
                      <%--  <tr>
                            <td class="tdleft1">
                                其他信息:<br />
                                (对记录的说明信息)
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtOtherNote" runat="server" TextMode="MultiLine" Height="50px"
                                    Width="70%"></asp:TextBox>
                                <input id="Text2" type="text" runat="server" readonly="readonly" style="display: none" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="tdleft1">
                                变更:
                            </td>
                            <td colspan="3">
                                <table width="100%" class="toptable grid">
                                    <tr class="toptable grid">
                                        <td align="center">
                                            <asp:Button ID="btnMarChange" runat="server" Text="物料/数量/重量修改" Enabled="false" ForeColor="Red"
                                                BackColor="Transparent" BorderStyle="None" CommandName="wszChg" OnClick="btnClick_Change"
                                                OnClientClick="return confirm('确认变更吗？','变更生成后无法取消,请仔细核对！！！')" />
                                            <br />
                                            <asp:CheckBox ID="ckbMarChange" runat="server" ToolTip="（按序号）当父级数量发生变化时，子级也按比例修改数量" />变更关联到下级
                                        </td>
                                      <%--  <td align="center">
                                            <asp:Button ID="btnAttChange" runat="server" Text="结构修改" Enabled="false" ForeColor="Red"
                                                BackColor="Transparent" BorderStyle="None" CommandName="attChg" OnClick="btnClick_Change"
                                                OnClientClick="return confirm('请谨慎操作！！！确认结构变更吗？','变更生成后无法取消,请仔细核对！！！');" />
                                            <br />
                                            <asp:CheckBox ID="ckbAttChange" runat="server" ToolTip="（按序号变更）" />变更关联到下级
                                        </td>--%>
                                   <td align="center">
                                            <asp:Button ID="btnDelete" runat="server" Text="单条删除" Enabled="false" ForeColor="Red"
                                                BackColor="Transparent" CommandName="singleDelete" BorderStyle="None" OnClick="btnClick_Change"
                                                OnClientClick="return confirm('请谨慎操作！！！确定变更删除吗？','变更生成后无法取消,请仔细核对！！！');" />
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnattDelete" runat="server" Text="结构删除" Enabled="false" ForeColor="Red"
                                                BackColor="Transparent" CommandName="attDelete" BorderStyle="None" OnClick="btnClick_Change"
                                                OnClientClick="return confirm('请谨慎操作！！！确定变更删除吗？','变更生成后无法取消,请仔细核对！！！');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdleft1">
                            </td>
                            <td colspan="3">
                             <%-- <input id="btnConfirmReal"  type="button" style="width: 40" value="确定" runat="server" onclick="SubmitData(this);"  />--%>
                         <asp:Button ID="btnConfirmReal" runat="server" Text="确定"  Width="40px" OnClick="btnConfirm_Click" />
                                
                         <%--  <asp:Button ID="btnConfirm" UseSubmitBehavior="true" runat="server" CssClass="hidden"  Width="0" Text="确定" OnClick="btnConfirm_Click" />--%>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="取消" Width="40" OnClick="btnCancel_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="confirmChange" runat="server" Text="激活变更" Width="55" OnClick="confirmChange_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancelMSChange" runat="server" Text="取消明细变更" Width="80" OnClick="btnCancelMSChange_OnClick"
                                    OnClientClick="return window.confirm('确认继续？','【取消明细变更】将更新相关明细，因此【请先确认该条记录是否正确】，需要注意的是该记录！！！');" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancelMpChange" runat="server" Text="取消计划变更" Width="80" OnClick="btnCancelMpChange_OnClick"
                                    OnClientClick="return window.confirm('确认继续？','【取消计划变更】时必须保证当前记录与已提交计划记录一致！！！');" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div>
                <input id="waixie" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="mpstate" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="msstate" type="text" runat="server" readonly="readonly" style="display: none" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: relative; bottom: 50%; right: -20%">
                <table width="60%">
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            数据处理中，请稍后...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>

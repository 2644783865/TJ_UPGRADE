<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Engineering_Reg.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Engineering_Reg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server"> 
    工程信息</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
 
<%--<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>--%>
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
<script type="text/javascript" charset="GB2312">
      function checkedNum(obj)
      {
            var text=obj.value;
            var pattem=/^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$/; 
            if(!pattem.test(text))
            {
                obj.style.background="yellow";
                obj.value="0";
                alert('输入格式有误!');
                return false;
            }
            else 
            {
                obj.style.background="Transparent";
            }
      }
 </script>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<div class="RightContent">
     <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%" >
                    <tr><td> 工程详细信息(带<span class="red">*</span>号的为必填项)</td></tr>
                 </table>
             </div>
         </div>
     </div>
<div>
    <%--<div style="height:6px" class="box_top"></div>--%>
    <%--<div class="box-outer">--%>
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <tr>
        <td class="tdleft1">生产制号:</td>     
        <td class="tdright1"><asp:Label ID="tsaid" runat="server"></asp:Label></td>
        <td class="tdleft1">项目名称:</td>          
        <td class="tdright1"><asp:Label ID="proname" runat="server"></asp:Label></td>
       </tr>
        <tr>
        <td class="tdleft1">工程名称:</td>     
        <td class="tdright1"><asp:Label ID="engname" runat="server"></asp:Label></td>
        <td class="tdleft1">工程类型:</td>     
        <td class="tdright1"><asp:Label ID="ddlengtype" runat="server"></asp:Label></td>
        </tr>
    </table>
    <%--</div>--%>
</div>     
<asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1">
<asp:TabPanel ID="TabPanel1" runat="server" HeaderText="任务分工信息" TabIndex="0">
<ContentTemplate>
    <div>
       <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <tr>
                <td class="tdleft1">图 号:</td>          
                <td class="tdright1"><asp:Label ID="drawcode" runat="server"></asp:Label></td>
                <td class="tdleft1">设备号:</td>          
                <td class="tdright1"><asp:Label ID="devicecode" runat="server"></asp:Label></td>
            </tr>
            
            <tr>
                <td class="tdleft1">设计单位:</td>          
                <td class="tdright1"><asp:Label ID="designcom" runat="server"></asp:Label></td>
                <td class="tdleft1">型 号:</td>          
                <td class="tdright1"><asp:Label ID="modelcode" runat="server"></asp:Label></td>
            </tr>

            <tr>
                <td class="tdleft1">接收日期:</td>          
                <td class="tdright1"><asp:Label ID="recvdate" runat="server"></asp:Label></td>
                <td class="tdleft1">合同完成日期:</td>          
                <td class="tdright1"><asp:Label ID="confishdate" runat="server"></asp:Label></td>
            </tr>
            
             <tr>
                <td class="tdleft1">类 型:</td>          
                <td class="tdright1"><asp:Label ID="contype" runat="server"></asp:Label></td>
                <td class="tdleft1">图纸状态:</td>          
                <td class="tdright1"><asp:Label ID="drawstate" runat="server"></asp:Label></td>
            </tr>
            
            <tr>
                <td class="tdleft1">委托人:</td>          
                <td class="tdright1"><asp:Label ID="client" runat="server"></asp:Label></td>
                <td class="tdleft1">技术负责人:</td>          
                <td class="tdright1"><asp:Label ID="labperson" runat="server"></asp:Label></td>
            </tr>
            
            <tr>
                <td class="tdleft1">任务开始时间:</td>     
                <td class="tdright1"><asp:Label ID="startdate" runat="server"></asp:Label></td>
                <td class="tdleft1">计划完成时间:</td>     
                <td class="tdright1"><asp:Label ID="planfishdate" runat="server"></asp:Label></td>
            </tr>
             
             <tr>
                <td class="tdleft1">实际完成时间:</td>     
                <td class="tdright1"><asp:Label ID="realfishdate" runat="server"></asp:Label></td>
                <td class="tdleft1">油漆计划:</td>     
                <td class="tdright1"><asp:Label id="planingplan" runat="server"></asp:Label></td>
             </tr>
             
             <tr>
                <td class="tdleft1">技术交底:</td>     
                <td class="tdright1"><asp:Label ID="techsharing" runat="server" ></asp:Label></td>
                <td class="tdleft1">计划准备进度:</td>     
                <td class="tdright1"><asp:Label id="plpreschedule" runat="server"></asp:Label></td>
             </tr>
             
             <tr>
                <td class="tdleft1">第三方:</td>     
                <td class="tdright1"><asp:Label id="thirdpart" runat="server"></asp:Label></td>
                <td class="tdleft1">任务状态:</td>     
                <td class="tdright1"><asp:Label id="rblstatus" runat="server"></asp:Label></td>
             </tr>

            <tr>
                <td class="tdleft1">备 注:</td>     
                <td colspan="3">
                <asp:TextBox ID="note" runat="server" TextMode="MultiLine" Rows="3" Width="100%" Enabled="false"></asp:TextBox></td>
            </tr>
        </table>
    </div>
</ContentTemplate>
</asp:TabPanel>
<asp:TabPanel ID="TabPanel2" runat="server" HeaderText="技术员登记信息" TabIndex="1">
<ContentTemplate>
    <div>
        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
           <tr>
            <td class="tdleft1">数 量:</td>     
            <td class="tdright1"><input id="quantity" runat="server" type="text" onblur="checkedNum(this)"/></td>
            <td class="tdleft1">总 重:</td>          
            <td class="tdright1"><input id="totalwght" runat="server" type="text" onblur="checkedNum(this)" /></td>
           </tr>
           <tr>
            <td class="tdleft1">体 积:</td>          
            <td class="tdright1"><input id="volume" runat="server" type="text" onblur="checkedNum(this)" /></td>
            <td class="tdleft1">厂内制作及外协带料部分重量:</td>          
            <td class="tdright1">
               <input id="cnwxdlzl" runat="server" type="text" onblur="checkedNum(this)" />&nbsp;&nbsp;
               <asp:Button ID="btnUpdate" runat="server" Text="更 新" OnClick="btnUpdate_OnClick"  OnClientClick="return confirm('确认更新吗？\r\r提示:更新重量为已提交计划的【板材】和【型材】材料总重之和');" />
            </td>
           </tr>
           <tr>
            <td class="tdleft1">净重量:</td>     
            <td class="tdright1"><input id="netwght" runat="server" type="text" onblur="checkedNum(this)" /></td>
            <td class="tdleft1">毛 重:</td>          
            <td class="tdright1"><input id="grosswght" runat="server" type="text" onblur="checkedNum(this)" /></td>
           </tr>
           <tr>
            <td class="tdleft1">材料计划完成时间:</td>     
            <td class="tdright1"><input id="caildate" runat="server" type="text"  /></td>
            <td class="tdleft1">外协外购件完成时间:</td>          
            <td class="tdright1"><input id="wgdate" runat="server" type="text" /></td>
           </tr>
           <tr>
            <td class="tdleft1">制作明细完成时间:</td>     
            <td class="tdright1"><input id="mxdate" runat="server" type="text" /></td>
            <td class="tdleft1">装箱单完成时间:</td>          
            <td class="tdright1"><input id="zxdate" runat="server" type="text" /></td>
           </tr>
           <tr>
            <td class="tdleft1">技术交底完成时间:</td>     
            <td class="tdright1"><input id="jsdate" runat="server" type="text" /></td>
            <td class="tdleft1">油漆计划完成时间:</td>          
            <td class="tdright1"><input id="oildate" runat="server" type="text" /></td>
           </tr>
           <tr>
                <td class="tdleft1">技术准备状态:</td>     
                <td >
                    <asp:RadioButtonList ID="rblJState" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                      <asp:ListItem Text="进行中" Value="1"></asp:ListItem>
                      <asp:ListItem Text="完工" Value="2"></asp:ListItem>
                      <asp:ListItem Text="停工" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
               <td class="tdleft1">技术准备完成时间:</td>     
                <td >
                    <asp:TextBox ID="txtWCSJ" runat="server" onclick="setday(this);"></asp:TextBox>
                </td>
            </tr>
           <tr>
                <td class="tdleft1">备 注:</td>     
                <td colspan="3">
                <asp:TextBox ID="record" runat="server" TextMode="MultiLine" Rows="3" Width="100%" ></asp:TextBox></td>
            </tr>
           <tr>
               <td class="tdleft1"></td>   
               <td colspan="3">
               <asp:Button ID="btnConfirm" runat="server" Text="确 定" onclick="btnConfirm_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnCancel" runat="server" Text="取 消" onclick="btnCancel_Click"/>                       
               </td>
            </tr>
        </table>
    </div>
</ContentTemplate>
</asp:TabPanel>
</asp:TabContainer>        
</asp:Content>

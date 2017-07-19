<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="TM_MSAdjustOrgInput.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MSAdjustOrgInput" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">总序拆分
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../JS/TMOrgSplit.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/ChoiceRcmd.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/TM_ShowInput.js" type="text/javascript" charset="GB2312"></script>
<script language="javascript" type="text/javascript">
    function getClientId()    {        var paraId1 = '<%= grvOrgData.ClientID %>';        var paraId2='<%= GridView1.ClientID %>';        var paraId3='<%= lblNumber.ClientID %>';        var paraId4='<%= txtBxishu.ClientID %>';        var paraId5='<%= txtXxishu.ClientID %>';        return {GrvOrg:paraId1,GrvSplit:paraId2,Number:paraId3,BXishu:paraId4,XXishu:paraId5};    }
    
    function okay() 
    {
        window.parent.document.getElementById('ButtonViewDone').click();
    }
    
   function cancel() 
   {
       window.parent.document.getElementById('ButtonViewCancel').click();
   }
   
</script>
 <div class="box-inner">
  <div class="box_right">
    <div class="box-title">
      <table width="100%">
       <tr>
         <td>台数:<asp:Label ID="lblNumber" runat="server" Text=""></asp:Label></td>
         <td align="right">
            <strong>拆分总序对应<span style="color:Red;">序号</span>:</strong><asp:TextBox ID="txtXuHao" runat="server" onfocus="this.select();"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;</td>
             <td align="right">拆分条数:
             <asp:DropDownList ID="ddlSplitNums" OnSelectedIndexChanged="ddlSplitNums_OnSelectedIndexChanged" AutoPostBack="true" runat="server">
                   <asp:ListItem Text="--2--" Value="2"></asp:ListItem>
                   <asp:ListItem Text="--3--" Value="3"></asp:ListItem>
                   <asp:ListItem Text="--4--" Value="4"></asp:ListItem>
                   <asp:ListItem Text="--5--" Value="5"></asp:ListItem>
                   <asp:ListItem Text="--6--" Value="6"></asp:ListItem>
                   <asp:ListItem Text="--7--" Value="7"></asp:ListItem>
                   <asp:ListItem Text="--8--" Value="8"></asp:ListItem>
                   <asp:ListItem Text="--9--" Value="9"></asp:ListItem>
                   <asp:ListItem Text="--10--" Value="10"></asp:ListItem>
                   <asp:ListItem Text="--11--" Value="11"></asp:ListItem>
                   <asp:ListItem Text="--12--" Value="12"></asp:ListItem>
                   <asp:ListItem Text="--13--" Value="13"></asp:ListItem>
                   <asp:ListItem Text="--14--" Value="14"></asp:ListItem>
                   <asp:ListItem Text="--15--" Value="15"></asp:ListItem>
                   <asp:ListItem Text="--16--" Value="16"></asp:ListItem>
                   <asp:ListItem Text="--17--" Value="17"></asp:ListItem>
                   <asp:ListItem Text="--18--" Value="18"></asp:ListItem>
                   <asp:ListItem Text="--19--" Value="19"></asp:ListItem>
                   <asp:ListItem Text="--20--" Value="20"></asp:ListItem>
                    <asp:ListItem Text="--其它--" Value="其它"></asp:ListItem>
             </asp:DropDownList></td><td align="left"><asp:TextBox ID="txtSplitNums" Text="2" Enabled="false" runat="server"></asp:TextBox></td>
          <td>
           <asp:Button ID="btnQuery" runat="server" OnClientClick='return confirm("页面所有信息将重置,确认吗？")' OnClick="btnQuery_Click" Text="查 询" /> &nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnClear" runat="server" OnClick="btnClear_OnClick" Text="清空重填" />
        </td>
        </tr>
     </table>
     <div>
 <table>
 <tr>
   <td align="right">

   </td>
 </tr>
</table>
     </div>
    </div>
   </div>
  </div> 
  <div class="box-wrapper">
     <div class="box-outer">
      <div style="vertical-align:bottom;">
      <table width="100%">
        <tr>
          <td><h5>拆分记录信息:</h5></td>
          <td id="td_marplan" runat="server">该条记录是否提计划:<asp:DropDownList ID="ddl_OWMarPlan" runat="server">
                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                <asp:ListItem Text="N" Value="N"></asp:ListItem>
              </asp:DropDownList>
          </td>
          <td>
              <asp:Button ID="btnWMarPlan" runat="server" Text="【是否提交计划】调换" OnClick="btnWMarPlan_OnClick" ToolTip="在拆分总项未提交计划的情况下，调换拆分总项与拆分明细提交计划状态" />
              &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ckbCheckWmarPlan" Checked="true" runat="server" />验证【是否提计划】
              </td>
          <td align="right">
               <span style="color:Red; font-size:larger;">如果要取消拆分，请点击这里关闭&nbsp;&nbsp;</span><a onclick="cancel();" style="font-family: Verdana, Helvetica, Arial, sans-serif;background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 3px;" title="关闭">X</a>
          </td>
        </tr>
      </table>
          <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
          </div>
          <div style="width:99%; overflow:scroll;overflow-y:auto; overflow-x:yes;">
                <asp:GridView ID="grvOrgData" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField HeaderText="序号" DataField="BM_XUHAO" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="图号(标识号)" DataField="BM_TUHAO" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="总序" DataField="BM_ZONGXU" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="中文名称" DataField="BM_CHANAME" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="物料编码" DataField="BM_MARID" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="规格" DataField="BM_MAGUIGE" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材质" DataField="BM_MAQUALITY" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="单台数量|总数量|计划数量" DataField="NUMBER" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="实际单重(kg)" ItemStyle-Wrap="false" DataFormatString="{0:f4}" HeaderStyle-Wrap="false"  DataField="BM_UNITWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="实际总重(kg)" ItemStyle-Wrap="false" DataFormatString="{0:f4}" HeaderStyle-Wrap="false"  DataField="BM_TOTALWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材料单重(kg)" ItemStyle-Wrap="false" DataFormatString="{0:f4}" HeaderStyle-Wrap="false"  DataField="BM_MAUNITWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材料总重(kg)" ItemStyle-Wrap="false" DataFormatString="{0:f4}" HeaderStyle-Wrap="false"  DataField="BM_MATOTALWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="长度(mm)" ItemStyle-Wrap="false" DataFormatString="{0:f2}" HeaderStyle-Wrap="false"  DataField="BM_MALENGTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="宽度(mm)" ItemStyle-Wrap="false" DataFormatString="{0:f2}" HeaderStyle-Wrap="false"  DataField="BM_MAWIDTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="总长(mm)" ItemStyle-Wrap="false" DataFormatString="{0:f2}" HeaderStyle-Wrap="false"  DataField="BM_MATOTALLGTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="面域|计划面域(m&sup2;)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  DataField="BM_MY" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_STANDARD" ItemStyle-Wrap="false" HeaderText="国标" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_PROCESS" ItemStyle-Wrap="false" HeaderText="工艺流程" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_KEYCOMS" ItemStyle-Wrap="false" HeaderText="关键部件" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_ISMANU" ItemStyle-Wrap="false" HeaderText="体现明细" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_NOTE" ItemStyle-Wrap="false" HeaderText="备注" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_MANAME" ItemStyle-Wrap="false" HeaderText="物料名称" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_THRYWGHT" ItemStyle-Wrap="false" HeaderText="理论重量" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField DataField="BM_MASHAPE" ItemStyle-Wrap="false" HeaderText="毛坯形状" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_FIXEDSIZE" ItemStyle-Wrap="false" HeaderText="是否定尺" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_MAUNIT" ItemStyle-Wrap="false" HeaderText="单位" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                      <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                     <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                     <EditRowStyle BackColor="#2461BF" />
                     <AlternatingRowStyle BackColor="White" /> 
                </asp:GridView>
                </div>
                <asp:Panel ID="NoDataPanel" runat="server"><span style="color:Red;">没有记录！</span>
                </asp:Panel>
                <br />
      <div style="vertical-align:bottom;">
      <table style="width:100%">
      <tr><td align="left"><h5>拆分后记录信息:</h5>
          <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red"></asp:Label>
      </td>
      <td align="center"><asp:Button ID="btnCheck" runat="server" Visible="false" OnClick="btnCheck_OnClick" Text="检 查" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" Visible="false" OnClientClick="return CheckNumNotZeroWithPurUnit(3)" OnClick="btnSave_OnClick" Text="保 存" /></td>
      </tr>
      <tr><td colspan="2">       
       <div style="width:100%; text-align:right;"><strong>计划系数:</strong>(板)<input id="txtBxishu" runat="server" type="text" onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.1";this.foucs();}' style="width:30px"  value="1.1" />(型/圆钢)<input id="txtXxishu" runat="server" style="width:30px"  onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.05";this.foucs();}'  type="text" value="1.05" />(其它)<input id="txtQxishu" runat="server" style="width:30px" disabled="disabled"  type="text" value="1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div></td></tr></table></div>     
       <div style="width:100%;">
            <asp:Panel ID="NoDataPanel2" runat="server"><span style="color:Red;">没有记录！</span>
            </asp:Panel>
            <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="4" ForeColor="#333333" Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号|明细序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate><%# Convert.ToInt32(Container.DataItemIndex +1) %>
                            <asp:TextBox ID="txtXuHao" onkeydown="UpDownLeftRight(this);" Width="100px"  ondblclick="ShowInput(this);" onfocus="GetIndex(this);this.select();" runat="server" Text='<%#Eval("BM_XUHAO") %>'></asp:TextBox>&nbsp;|
                            <asp:TextBox ID="txtMsXuHao" Width="100px"  ondblclick="ShowInput(this);" onfocus="this.select();" runat="server" Text='<%#Eval("BM_MSXUHAO") %>'></asp:TextBox><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="tuhao" runat="server" onkeydown="UpDownLeftRight(this);" ondblclick="ShowInput(this);" onfocus="this.select();" style="border-style: none; width: 150px" type="text" 
                                value='<%#Eval("BM_TUHAO") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <asp:TextBox ID="marid"  runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.blur();"  BorderStyle="None" ReadOnly="true" Text='<%#Eval("BM_MARID")%>' Width="90px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总序" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED" >
                        <ItemTemplate>
                            <input id="zongxu" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.blur();" readonly="readonly" style="border-style: none;
                                width: 80px" type="text" value='<%#Eval("BM_ZONGXU") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中文名称">
                        <ItemTemplate>
                            <input id="ch_name" runat="server" onkeydown="UpDownLeftRight(this);"  ondblclick="ShowInput(this);" onfocus="this.select();" style="border-style: none; width: 80px"
                                type="text" value='<%#Eval("BM_CHANAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <input id="beizhu" runat="server" onkeydown="UpDownLeftRight(this);"  ondblclick="ShowInput(this);" onfocus="this.select();" style="border-style: none; width: 100px" type="text"
                                value='<%#Eval("BM_NOTE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料长度" >
                        <ItemTemplate>
                            <input id="cailiaocd" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.select();" onchange="cal_len_width(this);" title="mm" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MALENGTH") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料宽度" >
                        <ItemTemplate>
                            <input id="cailiaokd" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.select();" title="mm" onchange="cal_len_width(this);" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MAWIDTH") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单台数量|总数量|计划数量" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="sing_shuliang" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.select();" onchange="cal_num(this);"  style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_SINGNUMBER") %>' />
                            |<input id="shuliang" runat="server" readonly="readonly" onfocus="this.blur();"  style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_NUMBER") %>' />  
                            |<input id="p_shuliang" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.select();" onchange="cal_pnum(this);"  style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_PNUMBER") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实际单重" >
                        <ItemTemplate>
                            <input id="dzh" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.select();" title="kg:制作明细" onchange="cal_unitweight(this);" style="border-style: none; width: 70px"
                                type="text" value='<%#Eval("BM_UNITWGHT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="实际总重" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="zongzhong" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.select();" readonly="readonly" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_TOTALWGHT") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总长" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="cailiaozongchang" onkeydown="UpDownLeftRight(this);" runat="server" onfocus="this.select();" onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="0";this.select();}'  style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_MATOTALLGTH") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="面域|计划面域(m2)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="bgzmy" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.select();" onchange="cal_my(this);" title="平方米:输入单个数量面域即可，不包含双面！！！" style="border-style: none;
                                width: 60px" type="text" value='<%#Eval("BM_MABGZMY") %>' />
                            <input id="mpmy" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.select();"  title="平方米:输入单个数量面域即可，不包含双面，用于提计划！！！" style="border-style: none;
                                width: 60px" type="text" value='<%#Eval("BM_MPMY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="guige" runat="server" onkeydown="UpDownLeftRight(this);"  ondblclick="ShowInput(this);" onfocus="this.select();"  style="border-style: none; width: 100px"
                                type="text" value='<%#Eval("BM_GUIGE") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料单重" >
                        <ItemTemplate>
                            <input id="cailiaodzh" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.select();" title="kg:材料计划" onchange="cal_marunitweight(this);" style="border-style: none; width: 70px"
                                type="text" value='<%#Eval("BM_MAUNITWGHT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="材料总重" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="cailiaozongzhong" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="this.select();"  onblur="var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert('请输入正确的数值！！！');this.value='0';this.select();}" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_MATOTALWGHT") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="ku" runat="server" onkeydown="UpDownLeftRight(this);" onfocus="findChoice(this,2,16);this.select();"  style="border-style: none; width: 30px"
                                type="text" value='<%#Eval("BM_KU") %>' />
                              <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square;text-align:left; line-height:normal;"></ul>
                                </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="提计划" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                          <asp:DropDownList ID="ddlWMarPlan"  runat="server" SelectedValue='<%#Eval("BM_WMARPLAN") %>'>
                             <asp:ListItem Text="N" Value="N" ></asp:ListItem>
                             <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                          </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>  
                   </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn TableHeight="300px" TableWidth="99%" FixRowType="Header,Pager" FixColumns="" />
            </yyc:SmartGridView>  
            </div>    
      </div>
  </div>
</asp:Content>

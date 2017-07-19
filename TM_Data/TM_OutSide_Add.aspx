<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMasterSession.Master" CodeBehind="TM_OutSide_Add.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_OutSide_Add" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="labprostru" runat="server" Text=""></asp:Label></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<JSR:JSRegister ID="JSRegister1" runat="server" />

    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/WGInput.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/TM_BlukCopy.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/ChoiceRcmd.js" type="text/javascript" charset="GB2312"></script>
    <script type="text/javascript" language="javascript" >
       function WGSave()
       {
          var table=document.getElementById('<%=GridView1.ClientID %>');
          var tr=table.getElementsByTagName("tr");
          for(var i=1;i<tr.length;i++)
          {
            var zongxu=tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value.trim();
            var type=tr[i].getElementsByTagName("td")[5].getElementsByTagName("select")[0];
            var caizhi=tr[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.trim();
            var guige=tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.trim();
            var dw=tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value.trim();
            if(zongxu!="")
            {
                if(dw=="")
                {
                   alert('第'+i+'行,单位不能为空！！！');
                   return false;
                }
                
                if(type.options[0].selected==true)
                {
                   alert('第'+i+'行,请选择部件类型！！！');
                   return false;
                }
                else if(dw=="平米"||dw=="平方米"||dw=="M2"||dw=="m2"||dw=="㎡")
                {
                   var mpmy=tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[1].value.trim();
                   if(mpmy==""||parseFloat(mpmy)<=0)
                   {  
                       alert('第'+i+'行,单位为平米，请输入【计划面域】！！！');
                       return false;
                   }
                }
            }
          }
          return true;
       }
       
        function getClientId()        {            var paraId1 = '<%= GridView1.ClientID %>';//注册控件1            var paraId2='<%= txtid.ClientID %>';            var paraId3='<%= istid.ClientID %>';            var paraId4='<%= lblNumber.ClientID %>';            return {Id1:paraId1,Id2:paraId2,Id3:paraId3,Id4:paraId4};//生成访问器        }
        
    </script>
     
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="96%">
                            <tr>
                                <td style="width: 20%">
                                    任务号:<asp:Label ID="tsaid" runat="server"></asp:Label>
                                    <input id="eng_type" type="text" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 20%">
                                    项目名称:<asp:Label ID="lab_proname" runat="server"></asp:Label>
                                    <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 20%">
                                    设备名称:<asp:Label ID="lab_engname" runat="server"></asp:Label>
                                </td>
                                <td  style="width: 20%">台数:<asp:TextBox ID="lblNumber" runat="server" Width="40px"></asp:TextBox></td>
                                <td>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/Basic_Data/WL_Material_List.aspx">
                                    <asp:Image ID="ImageTo" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                    查看物料信息</asp:HyperLink></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div>
            <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btninsert" runat="server" ToolTip="插入选择行下方,不超过15条！！！" Text=" 插 入" OnClientClick="return insert()" OnClick="btninsert_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btndelete" runat="server" Text="删 除" OnClientClick="return check()" OnClick="btndelete_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnsave" runat="server" Text="保 存" OnClientClick="return WGSave()" OnClick="btnsave_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<spn  style="color:Red;"></spn>
                    </td>
                    <td>
                        <asp:CheckBox ID="ckbAdd" runat="server" ToolTip="勾选后，在界面上录入总序“1.2”时也可录入“1.2.1”" />可录入归属物料</td>
                    <td align="left"><p>(1)、此页面输入物料为<span  style="color:Red;">加工件(01.08)</span>、<span  style="color:Red;">外购件(01.11)</span>及外协<span  style="color:Red;">非标标准件(01.11)</span>,物料编码自动生成;<br />(2)、<span  style="color:Red;">附图采购件需将图号输入规格列中</span>，即：物料规格-图号×××或图号×××；<br />(3)、<span  style="color:Red;">【单重】将保存为【理论重量】</span>，此页面输入【单位】<span  style="color:Red;">不能是</span>表示【长度】或【重量】的单位。</p></td>
                </tr>
            </table>
            </div>
            <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="1" ForeColor="#333333" Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="10px"
                                CssClass="checkBoxCss" />
                        </ItemTemplate>
                        <ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <input id="lblIndex" runat="server" readonly="readonly" style="border-style: none;
                                width: 30px" type="text" value="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图号(标识号)|助记码" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="tuhao" runat="server" onfocus="this.select();" onchange="WG_ChangeTuhao(this);"  onmousemove="this.title=this.value;" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 150px" type="text"
                                value='<%#Eval("BM_TUHAO") %>' />
                            |&nbsp;<input id="zjm" runat="server" onfocus="this.select();" onmousemove="this.title=this.value;" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 100px" type="text"
                                value='<%#Eval("BM_ZHUJIMA") %>' />    
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>               
                    <asp:TemplateField HeaderText="中文名称">
                        <ItemTemplate>
                            <input id="ch_name" runat="server" onfocus="this.select();" onmousemove="this.title=this.value;" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 100px" title="请在输入正确的名称！！！"
                                type="text" value='<%#Eval("BM_CHANAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="总序" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="zongxu" runat="server" onfocus="this.select();" onmousemove="this.title=this.value;" onkeydown="grControlFocusWithoutHiddden2(this);" onchange="ChangeZongxu(this)" style="border-style: none;
                                width: 90px" type="text" value='<%#Eval("BM_ZONGXU") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类型">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddltype" runat="server"  SelectedValue='<%#Eval("BM_TYPE") %>'>
                                <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                <asp:ListItem Text="采" Value="采"></asp:ListItem>
                                <asp:ListItem Text="铸" Value="铸"></asp:ListItem>
                                <asp:ListItem Text="锻" Value="锻"></asp:ListItem>
                                <asp:ListItem Text="协" Value="协"></asp:ListItem>
                                <asp:ListItem Text="非" Value="非"></asp:ListItem>
                                <asp:ListItem Text="板" Value="板"></asp:ListItem>
                                <asp:ListItem Text="型" Value="型"></asp:ListItem>
                                <asp:ListItem Text="圆" Value="圆"></asp:ListItem>
                                <asp:ListItem Text="采购成品" Value="采购成品"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单台数量|总数量|计划数量" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="shuliang" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" title="输入单台数量，系统自动乘以台数"  style="border-style: none;
                                width: 36px" type="text" value='<%#Eval("BM_NUMBER") %>' onchange="ChangeNum(this)" />
                                |&nbsp;<input id="total_shuliang" runat="server" onfocus="this.blur();" value='<%#Eval("BM_TOTALNUMBER") %>' class="notbrk" title="不能修改"  readonly="readonly" style="border-style: none;width: 36px" type="text" />
                                |&nbsp;<input id="p_shuliang" runat="server" value='<%#Eval("BM_PNUMBER") %>' onfocus="this.select();" class="notbrk" onchange="ChangeP_Num(this)" title="提材料计划数量" style="border-style: none;width: 36px" type="text" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="技术(采购)单位|辅助单位" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" >
                        <ItemTemplate>
                            <input id="labunit" runat="server" onfocus="this.select();" onmousemove="this.title=this.value;" onkeydown="grControlFocusWithoutHiddden2(this);" title="只能是【个数】单位"  style="border-style: none;
                                width:40px" type="text" value='<%#Eval("BM_MAUNIT") %>'  onchange="ChangeUnit(this)"  />|T
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="材料长度(mm)" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="cailiaochangdu" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none;
                                width: 80px" type="text" value='<%#Eval("BM_MALENGTH") %>'  />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料宽度(mm)" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="cailiaokuandu" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none;
                                width: 80px" type="text" value='<%#Eval("BM_MAWIDTH") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="下料备注" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="xialiaoNote" runat="server" onfocus="this.select();"  style="border-style: none;width: 80px" type="text" value='<%#Eval("BM_NOTE") %>'  />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                    
                    
                    <asp:TemplateField HeaderText="面域|计划面域(m2)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="my" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" title="单个数量面域"  style="border-style: none;
                                width:60px;" type="text" value='<%#Eval("BM_MABGZMY") %>' onchange="ChangeFloatNum(this);" />
                               |&nbsp;<input id="mpmy" runat="server" title="平方米:按【平米】采购计划，在此输入单个面域！！！" style="border-style: none;
                                width: 60px" type="text" value='<%#Eval("BM_MPMY") %>' onfocus="this.select();" onblur='var pattem=/^\d+(\.\d+)?$/; if(this.value!=""){ if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="";this.select();}}'  onkeydown='grControlFocusWithoutHiddden2(this.parentNode.getElementsByTagName("input")[0])'/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="图纸单重(kg)" >
                        <ItemTemplate>
                            <input id="dzh" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none; width: 80px"
                                type="text" value='<%#Eval("BM_TUUNITWGHT") %>' onchange="ChangeUweigh(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸总重(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="zongzhong" runat="server"  onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none;
                                width: 80px" type="text" value='<%#Eval("BM_TUTOTALWGHT") %>'  readonly="readonly" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="材料总长(mm)" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="cailiaozongchang" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none;
                                width: 80px" type="text" value='<%#Eval("BM_MATOTALLGTH") %>'  />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材质" >
                        <ItemTemplate>
                            <input id="caizhi" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MAQUALITY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="guige" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 120px"
                                type="text" value='<%#Eval("BM_GUIGE") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="国标" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="biaozhun" runat="server" readonly="readonly" style="border-style: none;
                                width: 150px" type="text" value='<%#Eval("BM_STANDARD") %>' onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                 
                    <asp:TemplateField HeaderText="图纸上材质">
                        <ItemTemplate>
                            <input id="tucz" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUMAQLTY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                    <asp:TemplateField HeaderText="图纸上问题">
                        <ItemTemplate>
                            <input id="tuwt" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUPROBLEM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="下料方式" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="xialiao" runat="server" onfocus="this.select();" style="border-style: none; width: 32px" type="text" value='<%#Eval("BM_XIALIAO") %>'
                                onkeydown="grControlFocus(this)" />
                            <div class="hidden" style="position: absolute; background-color: #f3f3f3; cursor: hand;
                                border: #B9D3EE 3px solid; padding: 0px; margin: 0px;">
                                <ul style="list-style-type: square; text-align: left; line-height: normal;">
                                </ul>
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="工艺流程">
                        <ItemTemplate>
                            <input id="process" runat="server" onfocus="findChoice(this,3,21);this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 100px" type="text"
                                value='<%#Eval("BM_PROCESS") %>' />
                                   <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square; text-align:left; line-height:normal;"></ul>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <input id="beizhu" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 120px" type="text"
                                value='<%#Eval("BM_ALLBEIZHU") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
       <asp:TemplateField HeaderText="制作明细">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlKeyComponents" runat="server" onkeydown="grControlFocus(this)"
                                SelectedValue='<%#Eval("BM_ISMANU") %>'>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="定尺">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlFixedSize" runat="server" onkeydown="grControlFocus(this)"
                                onchange="CtrlFixSize(this);" SelectedValue='<%#Eval("BM_FIXEDSIZE") %>'>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="提计划" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                             <asp:DropDownList ID="ddlWmarPlan"  runat="server" SelectedValue='<%#Eval("BM_WMARPLAN") %>'>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="N" Value="N" ></asp:ListItem>
                             </asp:DropDownList>
                        </ItemTemplate>
                        </asp:TemplateField>
                   </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn  TableHeight="480px" TableWidth="100%" FixColumns="0,1,2,3" />
            </yyc:SmartGridView>
            <%-- </asp:Panel>--%>
            <div>
                <input id="txtid" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="add_id" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="istid" type="text" runat="server" readonly="readonly" style="display: none" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 30%; right: 40%">
                <table>
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
</asp:Content>
